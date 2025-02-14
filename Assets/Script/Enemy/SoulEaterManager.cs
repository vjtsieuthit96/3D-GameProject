using System.Collections;
using System.Threading;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class SoulEaterManager : MonoBehaviour

{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;    

    private Vector3 _initPosition;
    private bool _isReturningToInitPosition;    
    [SerializeField] private float attackRange;

    [SerializeField] private Animator soulEaterAnimator;
    private int _speedHash;
    private int _attackHash;
    private int _skillHash;
    private int _isFlyingHash;
    private int _isRunningHash;
    private int _getHitHash;
    private int _isAliveHash;
    private int _immuneToGetHitHash;
    private int _getHitCount;

    private Coroutine _wanderCoroutine;

    [SerializeField] private Health dragonHealth;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    [SerializeField] private GameObject hpCanvas;
      
    
    // Phạm vi tấn công
    // quay về vị trí khi ko có target
    // tự động di chuyển xung quanh khu vực
    void Start()
    {

        _initPosition = transform.position;
        _speedHash = Animator.StringToHash("Speed");
        _attackHash = Animator.StringToHash("Attack");
        _skillHash = Animator.StringToHash("Skill");
        _isFlyingHash = Animator.StringToHash("isFlying");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isAliveHash = Animator.StringToHash("isAlive");
        _getHitHash = Animator.StringToHash("GetHit");
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
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

            if(_wanderCoroutine != null)
            {
                StopCoroutine(_wanderCoroutine);
                _wanderCoroutine = null;
            }
            // đuổi theo nhân vật            
            _isReturningToInitPosition = false;
            navMeshAgent.SetDestination(target.position);
            Fly();
            if (distanceAtk <= Constans.distanceNearPlayer)
            {  
                WalkorRun();
                if (distanceAtk <= Constans.distanceCanAtk)
                    soulEaterAnimator.SetTrigger(_attackHash);
            }
            
        }

        else
        {            
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
            Debug.Log("Get Hit");
            // Nếu Dragon chạy anim GetHit quá 3 lần sẽ ko chạy anim GetHit trong thời gian 5s;
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
        soulEaterAnimator.SetBool(_isRunningHash, false);
        soulEaterAnimator.SetBool(_isFlyingHash, true);
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
            yield return new WaitForSeconds(Random.Range(3, 7));
            var randomPosition = Random.insideUnitSphere * 7;
            randomPosition += _initPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, 7, NavMesh.AllAreas);
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
        var damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, hpCanvas.transform);
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
