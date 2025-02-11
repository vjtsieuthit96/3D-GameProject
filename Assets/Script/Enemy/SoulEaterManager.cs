using System.Collections;
using System.Threading;
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

    private Coroutine _wanderCoroutine;

    [SerializeField] private Health dragonHealth;
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
    }

    void Update()
    {            
        //tính khoảng cách của player đến vị trí ban đầu của quái 

        var distance = Vector3.Distance(_initPosition, target.position);
        
        // tính khoảng cách có thể attack của quái vật
        var distanceAtk = Vector3.Distance(transform.position, target.position);
        

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
            if (distanceAtk <= 3.5f)
            {  
                WalkorRun();
                if (distanceAtk <=2f)
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
        soulEaterAnimator.SetFloat(_speedHash, navMeshAgent.velocity.magnitude);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            Debug.Log("Get Hit");

            //lấy chỉ số dame từ vkhí
            //var damage = other.GetComponent<Weapon>().damage
            //trừ máu 
            dragonHealth.TakeDamage(10);

            // hết máu chết
            // nav mesh agent stop
        }
    }

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
 
   
}
