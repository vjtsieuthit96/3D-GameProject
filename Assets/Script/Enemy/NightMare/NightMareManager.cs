using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class NightMareManager : MonoBehaviour

{
    //Fields
    #region Fields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    [SerializeField] private NightMareSkillManager nightMareSkillManager;

    private Vector3 _initPosition;   
    private bool _isReturningToInitPosition;
    [SerializeField] private float attackRange;
    [SerializeField] private Animator nightMareAnimator;
    private int _speedHash;
    private int _attackHash;    
    private int _getHitHash;
    private int _isAliveHash;
    private int _immuneToGetHitHash;
    private int _getHitCount;
    private int _roarHash;
    
    
    
    private bool _playerInRange = false;
    private bool _jumpToTarget = true;
    private Coroutine _wanderCoroutine;

    [SerializeField] private Health dragonHealth;
    [SerializeField] GameObject damageTextPrefab;
    [SerializeField] private Transform floatingDamageSpawnPoint;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    [SerializeField] private GameObject hpCanvas;
    #endregion
   
    void Start()
    {
        _initPosition = transform.position;
        _speedHash = Animator.StringToHash("speed");
        _attackHash = Animator.StringToHash("attack");
        _isAliveHash = Animator.StringToHash("isAlive");
        _getHitHash = Animator.StringToHash("getHit");
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
        _roarHash = Animator.StringToHash("roar");  
        nightMareAnimator.SetBool(_isAliveHash, true);
    }

    void Update()
    {   
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
                nightMareAnimator.SetTrigger(_roarHash);
                _playerInRange = true;
            }

            if (_wanderCoroutine != null)
            {
                StopCoroutine(_wanderCoroutine);
                _wanderCoroutine = null;
            }
            // đuổi theo nhân vật            
            _isReturningToInitPosition = false;
            navMeshAgent.SetDestination(target.position);
            
            if (distanceAtk <= 20.0f && _jumpToTarget)
            {                
                nightMareSkillManager.TryCastJumpSkill();                
            }
            if (distanceAtk <= Constans.distanceNearPlayer)
            {
                _jumpToTarget = false;
                if (distanceAtk <= Constans.distanceCanAtk)
                    nightMareAnimator.SetTrigger(_attackHash);
            }
            else 
            {
                _jumpToTarget = true;
            }

        }

        else
        {
            _playerInRange = false;
            //quay về vị trí
            if (!_isReturningToInitPosition)
            {
                
                navMeshAgent.SetDestination(_initPosition);
                _isReturningToInitPosition = true;
            }
            // nếu quái cách vị trí ban đầu là 15 thì bắt đầu di chuyển
            if (Vector3.Distance(_initPosition, transform.position) <= 15)
            {
                if (_wanderCoroutine == null)
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
        nightMareAnimator.SetFloat(_speedHash, navMeshAgent.velocity.magnitude);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            Debug.Log("Enemy Is Hit");
            // Nếu Dragon chạy anim GetHit quá số lần cho phép sẽ ko chạy anim GetHit trong thời gian 5s;
            GetHit();

            if (nightMareAnimator.GetBool(_isAliveHash))
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
        }
    }

    #endregion
    // Logic Get Hit & Die
    #region enemyGetHit&Dead
    private void GetHit()
    {
        if (!nightMareAnimator.GetBool(_immuneToGetHitHash))
        {
            nightMareAnimator.SetTrigger(_getHitHash);
            _getHitCount++;
            if (_getHitCount >= Constans.hitCountToImmune)
            {
                nightMareAnimator.SetBool(_immuneToGetHitHash, true);
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
        nightMareAnimator.SetBool(_immuneToGetHitHash, false);
        _getHitCount = 0;
    }
    private void Die()
    {
        // Hết máu chết
        // Stop Navmesh Agent
        if (nightMareAnimator.GetBool(_isAliveHash))
        {
            nightMareAnimator.SetBool(_immuneToGetHitHash, true);
            nightMareAnimator.SetBool(_isAliveHash, false);
            navMeshAgent.isStopped = true;
            Destroy(gameObject, Constans.dispawnTime);
        }
    }
    #endregion
}
