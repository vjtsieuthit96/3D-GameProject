using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class SoulEaterManager : MonoBehaviour

{
    //Fields
    #region Fields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    [SerializeField] private SoulEaterSkillManager soulEaterSkillManager;   

    private Vector3 _initPosition;    
    private bool _isReturningToInitPosition;    
    [SerializeField] private float attackRange;    
    [SerializeField] private Animator soulEaterAnimator;
    private int _speedHash;
    private int _attackHash;   
    private int _isFlyingHash;
    private int _isRunningHash;
    private int _getHitHash;
    private int _isAliveHash;
    private int _immuneToGetHitHash;
    private int _getHitCount;
    private int _roarHash;

    private bool _canFly = true;
    private bool _playerInRange = false;
    [SerializeField] private float _flyCD = 15f;

    private Coroutine _wanderCoroutine;

    [SerializeField] private Health dragonHealth;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] private Transform floatingDamageSpawnPoint;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    [SerializeField] private GameObject hpCanvas;    
    #endregion

    // Phạm vi tấn công
    // quay về vị trí khi ko có target
    // tự động di chuyển xung quanh khu vực
    void Start()
    {
        _initPosition = transform.position;
        _speedHash = Animator.StringToHash("Speed");
        _attackHash = Animator.StringToHash("Attack");      
        _isFlyingHash = Animator.StringToHash("isFlying");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isAliveHash = Animator.StringToHash("isAlive");
        _getHitHash = Animator.StringToHash("GetHit");
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
        _roarHash = Animator.StringToHash("Roar");
        soulEaterAnimator.SetBool(_isAliveHash, true);
    }

    void Update()
    {   //Nếu player Die quay về vị trí ban đầu và bỏ target
       
        //tính khoảng cách của player đến vị trí ban đầu của quái 

        var distance = Vector3.Distance(_initPosition, target.position);
        
        // tính khoảng cách có thể attack của quái vật
        var distanceAtk = Vector3.Distance(transform.position, target.position);

        // Các logic về khoảng cách target & attack Player
        #region Logic NavmeshAgent
        if (distance <= attackRange)
        {
            if (!_playerInRange)
            { 
                soulEaterAnimator.SetTrigger(_roarHash);
                _playerInRange = true;
            }                    

            if(_wanderCoroutine != null)
            {
                StopCoroutine(_wanderCoroutine);
                _wanderCoroutine = null;
            }
            // đuổi theo nhân vật            
            _isReturningToInitPosition = false;
            navMeshAgent.SetDestination(target.position);
            Fly();
            if (distanceAtk < attackRange * 0.8f)
            {
                soulEaterSkillManager.TryCastFireBall();
            }
            if (distanceAtk <= Constans.distanceNearPlayer)
            {  
                WalkorRun();
                if (distanceAtk <= Constans.distanceCanAtk)
                    soulEaterAnimator.SetTrigger(_attackHash);
            }
            
        }

        else
        {
            _playerInRange = false;
            //quay về vị trí
            if (!_isReturningToInitPosition)
            {
                Fly();
                navMeshAgent.SetDestination(_initPosition);
                _isReturningToInitPosition=true;
            }
            // nếu quái cách vị trí ban đầu là 15 thì bắt đầu di chuyển
            if (Vector3.Distance(_initPosition, transform.position) <= 15)
            {                
                if(_wanderCoroutine == null)
                {
                    _wanderCoroutine = StartCoroutine(Wander());
                }
            }
            else
            {              
                
                if (_wanderCoroutine != null)
                {
                    StopCoroutine(_wanderCoroutine);
                    _wanderCoroutine = null;
                }
            }
        }
        #endregion
        
        //
        soulEaterAnimator.SetFloat(_speedHash, navMeshAgent.velocity.magnitude);         

    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            Debug.Log("Enemy Is Hit");
            // Nếu Dragon chạy anim GetHit quá số lần cho phép sẽ ko chạy anim GetHit trong thời gian 5s;
            GetHit();

            if (soulEaterAnimator.GetBool(_isAliveHash))
            {
                ShowFloatingDame();
                
                if (dragonHealth.GetCurrentHealth() <= 0)
                {
                    Die();
                }
            }            
        }
    }
    
    // Phương thức enemy di chuyển và logic Wander
    #region enemyWalkStyle & Wander
    private void Fly ()
    {
        if(_canFly)
        StartCoroutine(TrytoFly());
    }
    private IEnumerator TrytoFly()
    {
        _canFly = false;
        soulEaterAnimator.SetBool(_isRunningHash, false);
        soulEaterAnimator.SetBool(_isFlyingHash, true);
        yield return new WaitForSeconds(_flyCD);
        _canFly = true;
    }
    private void WalkorRun()
    {
        soulEaterAnimator.SetBool(_isRunningHash, true);
        soulEaterAnimator.SetBool(_isFlyingHash, false);
    }
    private IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            var randomPosition = Random.insideUnitSphere * 15;
            randomPosition += _initPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, 15, NavMesh.AllAreas);
            navMeshAgent.SetDestination(hit.position);
            WalkorRun();
        }
    }
    
    #endregion 
    // Logic Get Hit & Die
    #region enemyGetHit&Dead
    private void GetHit()
    {
        if (!soulEaterAnimator.GetBool(_immuneToGetHitHash))
        {
            soulEaterAnimator.SetTrigger(_getHitHash);
            _getHitCount++;
            if (_getHitCount >= Constans.hitCountToImmune)
            {
                soulEaterAnimator.SetBool(_immuneToGetHitHash, true);
                StartCoroutine(RemoveImmunityAfterCD());
            }
        }
    }     
    private void ShowFloatingDame()
    {
        //lấy chỉ số dame từ vkhí
        //var damage = other.GetComponent<Weapon>().damage
        //trừ máu
        var damage = Random.Range(15, 55);
        var damageText = Instantiate(damageTextPrefab, floatingDamageSpawnPoint.position + new Vector3(0, 0.5f, 0), Quaternion.identity, hpCanvas.transform);
        damageText.GetComponent<FloatingDamage>().SetText(damage);
        damageText.GetComponent<FloatingDamage>().SetCamera(_cinemachineFollow);
        dragonHealth.TakeDamage(damage);
    }
    private IEnumerator RemoveImmunityAfterCD()
    {
        yield return new WaitForSeconds(Constans.immuneDuration);
        soulEaterAnimator.SetBool(_immuneToGetHitHash, false);
        _getHitCount = 0;
    }
    private void Die()
    {
        // Hết máu chết
        // Stop Navmesh Agent
        if (soulEaterAnimator.GetBool(_isAliveHash))
        {
            soulEaterAnimator.SetBool(_immuneToGetHitHash, true);
            soulEaterAnimator.SetBool(_isAliveHash, false);
            navMeshAgent.isStopped = true;
            Destroy(gameObject, Constans.dispawnTime);
        }
    }
    #endregion
    

}
