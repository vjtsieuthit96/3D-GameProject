using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
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
    [SerializeField] protected CinemachineFollow _cinemachineFollow;
    [SerializeField] protected GameObject hpCanvas;    
    private Vector3 _initPosition; 
    private bool _isReturningToInitPosition;      
    private Coroutine _wanderCoroutine;
    private bool _isFlyingCoroutineRunning;
    private bool _isFlying;
    private bool _playerInRange;
    private int _getHitCount = 0;
    private int _roarHash;
    private int _isDeadHash;
    private int _speedHash;
    private int _isFlyingHash;
    private int _getHitHash;
    private int _immuneToGetHitHash;
    private int _turnHash;
    //giá trị hiển thị khi chọn type;
    public enum MonsterType
    {
        Flying,
        Ground
    }
    [SerializeField] protected MonsterType monsterType;
    [SerializeField, EnumCondition("monsterType", (int)MonsterType.Flying)]
    protected float _flyHeight;
    //
    #endregion

    protected virtual void Start()
    {
        _initPosition = transform.position;
        _roarHash = Animator.StringToHash("Roar");
        _isDeadHash = Animator.StringToHash("isDead");
        _speedHash = Animator.StringToHash("speed");
        _isFlyingHash = Animator.StringToHash("isFlying");
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
        _getHitHash = Animator.StringToHash("getHit");
        _turnHash = Animator.StringToHash("turn");
    }
    protected virtual void Update()
    {
        var distance = Vector3.Distance(_initPosition, target.position);
        var distanceAtk = Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            _attackTime += Time.deltaTime;
            if (!_playerInRange)
            {
                monsterAnimator.SetTrigger(_roarHash);
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
            _attackTime -= Time.deltaTime;
            _playerInRange = false;
            if (_isReturningToInitPosition)
            {
                navMeshAgent.SetDestination(_initPosition);
                _isReturningToInitPosition = true;
            }

            if (Vector3.Distance(_initPosition, transform.position) <= attackRange / 3)
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
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            Debug.Log("Enemy Is Hit");            
            if(!monsterAnimator.GetBool(_isDeadHash))
            {
                getHit();
                ShowFloatingDame();
                if(monsterHealth.GetCurrentHealth() <= 0)
                {
                    Die();
                }
            }
        }
    }
    //Monster Behavior
    #region Monster Behavior
    protected void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
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
                yield return new WaitForSeconds(Random.Range(3, 5));
                var randomPosition = Random.insideUnitSphere * 15;
                randomPosition += _initPosition;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomPosition, out hit, 15, NavMesh.AllAreas);
                navMeshAgent.SetDestination(hit.position);
            }
        }
    }
    protected IEnumerator Flying()
    {
        _isFlyingCoroutineRunning = true;
        monsterAnimator.SetBool(_isFlyingHash, true);
        _isFlying = true;
        yield return new WaitForSeconds(60f);
        monsterAnimator.SetBool(_isFlyingHash, false);
        _isFlying = false;
        _isFlyingCoroutineRunning = false;
    }
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

    //Monster Get Hit & Die
    #region MonsterIsHit&Die
    protected void getHit()
    {
        if (!monsterAnimator.GetBool(_immuneToGetHitHash))
        {
            monsterAnimator.SetTrigger(_getHitHash);
            _getHitCount++;
            if (_getHitCount >= Constans.hitCountToImmune)
            {
                monsterAnimator.SetBool(_immuneToGetHitHash, true);
                StartCoroutine(RemoveImmunityAfterCD());
            }
        }
    }
    protected IEnumerator RemoveImmunityAfterCD()
    {
        yield return new WaitForSeconds (Constans.immuneDuration);
        monsterAnimator.SetBool(_immuneToGetHitHash, false);
        _getHitCount = 0;
    }
    protected void ShowFloatingDame()
    {
        var damage = Random.Range(15, 55);// Để tạm sau này có damage của nhân vật thì bỏ vào
        var damageText = Instantiate(damageTextPrefab,floatingDamageSpawnPoint.position  
            + new Vector3 (0,0.5f,0),Quaternion.identity,hpCanvas.transform);
        damageText.GetComponent<FloatingDamage>().SetText(damage);
        damageText.GetComponent<FloatingDamage>().SetCamera(_cinemachineFollow);
        monsterHealth.TakeDamage(damage);
    }
    protected void Die()
    {
        monsterAnimator.SetBool(_isDeadHash, true);
        navMeshAgent.isStopped = true;
        Destroy(gameObject,Constans.dispawnTime);
    }
    #endregion
}
