using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class MonsterManager : MonoBehaviour
{
    
    //Fields
    #region Fields
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Transform target;    
    [SerializeField] protected Animator monsterAnimator;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float _attackTime = 0f;
    [SerializeField] protected Health monsterHealth;
    [SerializeField] protected GameObject damageTextPrefab;
    [SerializeField] protected Transform floatingDamageSpawnPoint;
    [SerializeField] protected CinemachineCamera _cinemachineFollow;
    [SerializeField] protected GameObject hpCanvas;
    [SerializeField] protected float _flyHeight;
    private Vector3 _initPosition; 
    private bool _isReturningToInitPosition;      
    private Coroutine _wanderCoroutine;
    private bool _isFlyingCoroutineRunning;
    protected bool _isFlying;
    public bool IsFlying() => _isFlying;
    private bool _playerInRange;   
    private int _roarHash;    
    private int _speedHash;
    protected int _isFlyingHash;
    protected int _isFallingHash;
    private int _noDangerHash;
    private int _posXHash;
    private int _posYHash;
    protected float distanceAtk;
    private bool _isDead;
    public bool IsDead() => _isDead;
    protected int _getHitHashType1;
    public int getHitHashType1() => _getHitHashType1;
    protected int _getHitHashType2;
    public int getHitHashType2() => _getHitHashType2;
    protected int _getHitHashFlying;
    public int getHitHashFlying() => _getHitHashFlying;
    protected int _immuneToGetHitHash;
    protected int _getHitCount;
    protected int _countToFall;   
    public int GetHitCountValue()=>_getHitCount;
    protected int _isDeadHash;    
    //giá trị hiển thị khi chọn type;
    public enum MonsterType
    {
        Flying,
        Ground
    }   
    [SerializeField] protected MonsterType monsterType;
      
    //
    #endregion

    protected virtual void Start()
    {
        _initPosition = transform.position;
        _roarHash = Animator.StringToHash("roar");        
        _speedHash = Animator.StringToHash("speed");
        _isFlyingHash = Animator.StringToHash("isFlying");        
        _noDangerHash = Animator.StringToHash("noDanger");
        _posXHash = Animator.StringToHash("pos_X");
        _posYHash = Animator.StringToHash("pos_Y");
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
        _getHitHashType1 = Animator.StringToHash("getHitType1");
        _getHitHashType2 = Animator.StringToHash("getHitType2");
        _getHitHashFlying = Animator.StringToHash("getHitFlying");
        _isDeadHash = Animator.StringToHash("isDead");
        _isFallingHash = Animator.StringToHash("isFalling");
    }
    protected virtual void Update()
    {
        var distance = Vector3.Distance(_initPosition, target.position);
        distanceAtk = Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            monsterAnimator.SetBool(_noDangerHash, false);
            _attackTime += Time.deltaTime;
            if (!_playerInRange)
            {
                if (!_isFlying)
                {                    
                    monsterAnimator.SetTrigger(_roarHash);
                }
                _playerInRange = true;
            }
            _isReturningToInitPosition = false;
            if (monsterType == MonsterType.Flying)
            {
                if (!_isFlying)
                {
                    navMeshAgent.SetDestination(target.position);
                    if (_wanderCoroutine != null)
                    {
                        StopCoroutine(_wanderCoroutine);
                        _wanderCoroutine = null;
                    }
                    if (_attackTime > 90f && !_isFlyingCoroutineRunning)
                    {
                        StartCoroutine(Flying());
                    }
                }
                else
                {
                    if (_wanderCoroutine == null)
                    {
                        _wanderCoroutine = StartCoroutine(Wander());
                    }
                }
            }
            else
            {
                navMeshAgent.SetDestination(target.position);
            }           
        }
        else
        {           
            monsterAnimator.SetBool(_noDangerHash, true);
            if (_attackTime > 0)
            {
                _attackTime -= Time.deltaTime;
            }
            else
            {
                _attackTime = 0;
            }
            _playerInRange = false;
            if (!_isReturningToInitPosition)
            {
                navMeshAgent.SetDestination(_initPosition);
                _isReturningToInitPosition = true;
            }

            if (Vector3.Distance(_initPosition, transform.position) <= 40)
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
        monsterAnimator.SetFloat(_speedHash, navMeshAgent.velocity.magnitude);
        if (monsterHealth.GetCurrentHealth() <= 0)
        {
            Destroy(gameObject,Constans.dispawnTime);
            _isDead = true;
        }
        if(_getHitCount >=5f)
        {
            monsterAnimator.SetBool(_immuneToGetHitHash, false);
        }
        else
        {
            monsterAnimator.SetBool(_immuneToGetHitHash, true);
        }
        if (_countToFall >=2)
        {
            monsterAnimator.SetBool(_isFallingHash, true);
            monsterAnimator.SetBool(_isFlyingHash, false);
            _isFlying = false;
            _countToFall = 0;
        }
    }

    protected virtual void LateUpdate()
    {
        var deltaPosition = (target.position - transform.position).normalized;
        monsterAnimator.SetFloat(_posXHash, deltaPosition.x);
        monsterAnimator.SetFloat(_posYHash, deltaPosition.z);
    }
    
    //Monster Behavior
    #region Monster Behavior
    protected virtual void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
    protected virtual void BackToNormalLook()
    {
        Vector3 direction = navMeshAgent.velocity.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,Time.deltaTime* 2f);
    }
    protected IEnumerator Wander()
    {
        while (true)
        {
            if (_isFlying)
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3f));
                var randomPosition = Random.insideUnitSphere * 30;
                randomPosition += _initPosition;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomPosition, out hit, 30, NavMesh.AllAreas);
                navMeshAgent.SetDestination(hit.position);
            }
            else
            {
                
                yield return new WaitForSeconds(Random.Range(3f, 5f));                
                var randomPosition = Random.insideUnitSphere * 20;
                randomPosition += _initPosition;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomPosition, out hit, 20, NavMesh.AllAreas);
                navMeshAgent.SetDestination(hit.position);
            }
        }
    }
    protected virtual IEnumerator Flying()
    {
        _isFlyingCoroutineRunning = true;
        monsterAnimator.SetBool(_isFlyingHash, true);
        _isFlying = true;
        yield return new WaitForSeconds(SetFlyTime());
        monsterAnimator.SetBool(_isFlyingHash, false);
        _isFlying = false;
        _isFlyingCoroutineRunning = false;
    }
    protected virtual float SetFlyTime() => 60f;
    protected IEnumerator AdjustHeightOverTime(float targetHeight, float duration)
    {
        float startHeight = navMeshAgent.baseOffset;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            navMeshAgent.baseOffset = Mathf.Lerp(startHeight, targetHeight, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        navMeshAgent.baseOffset = targetHeight;
    }
    #endregion;  
    protected void _ShowFloatingDame(float damage)
    {
        var damageText = Instantiate(damageTextPrefab, floatingDamageSpawnPoint.position
            + new Vector3(0, 0.5f, 0), Quaternion.identity, hpCanvas.transform);
        damageText.GetComponent<FloatingDamage>().SetText(damage);
        damageText.GetComponent<FloatingDamage>().SetCamera(_cinemachineFollow);
        monsterHealth.TakeDamage(damage);
    }
    public void ShowFloatingDamage(float damage) => _ShowFloatingDame(damage);  
    private bool _GetBoolImmuneToGetHit()
    {
        return monsterAnimator.GetBool(_immuneToGetHitHash);
    }
    public bool GetBoolImmuneToGetHit() => _GetBoolImmuneToGetHit();   
    
    private void _SetGetHitCount (int value)
    {
       _getHitCount = value;
    }
    public void SetGetHitCount (int value) =>_SetGetHitCount(value);
    private void _SetCountToFall(int value)
    {
        _countToFall = value;
    }

    public void SetCountToFall(int value) => _SetCountToFall(value);
    private void _countGetHit()
    {
        _getHitCount++;
    }
    public void countGetHit()=>_countGetHit();
    private void _CountToFall()
    {
        _countToFall++;
    }
    public void CountToFall() => _CountToFall();

    protected void _Die()
    {        
        navMeshAgent.isStopped = true;      
        monsterAnimator.SetBool(_isDeadHash, true);
    } 
    public void Die() => _Die();
    private bool _GetDeadHash()
    {
        return monsterAnimator.GetBool(_isDeadHash);
    }
    public bool GetBoolDeadHash()=>_GetDeadHash();
    private void _SetGetHit (int type)
    {
        monsterAnimator.SetTrigger(type);
    }
    public void SetGetHit (int type) => _SetGetHit(type);
    protected virtual void _StopMovement()
    {
        navMeshAgent.isStopped = true;
    }   
    protected virtual void _ResumeMovement()
    {
        navMeshAgent.isStopped = false;
    }
  
    
}
