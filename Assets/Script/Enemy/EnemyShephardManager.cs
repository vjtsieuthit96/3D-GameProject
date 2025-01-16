using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyShephardManager : MonoBehaviour
    
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;

    private Vector3 _initPosition;
    private Vector3 _lastPosition;
    //private Vector3 _lastPosition;
    [SerializeField] private float attackRange;
    [SerializeField] private Animator enemyAnimator;
    private int _speedHash;
    private int _attackHash;
    private int _isAliveHash;
    


    // Phạm vi tấn công
    // quay về vị trí khi ko có target
    // tự động di chuyển xung quanh khu vực
    void Start()
    {
        
        _initPosition = transform.position;
        _speedHash = Animator.StringToHash("Speed");
        _attackHash = Animator.StringToHash("Attack");
        _isAliveHash = Animator.StringToHash("IsAlive");
    }
    
    void Update()
    {      
                       
        //tính khoảng cách
        
        var distance = Vector3.Distance(_initPosition,target.position);
        
        if (distance <= attackRange)
        {
               
            StopCoroutine(Wander());
            // đuổi theo nhân vật            
            navMeshAgent.SetDestination(target.position);
            // tính phạm vi tấn công
            var distanceAtk = Vector3.Distance(transform.position, target.position);
            
            if (distanceAtk <= 2)
            {                
                enemyAnimator.SetTrigger(_attackHash);
            }
        }
        else
        {
            
            //quay về vị trí
            navMeshAgent.SetDestination(_initPosition);
            // nếu quái cách vị trí ban đầu là 5 thì bắt đầu di chuyển
            if (Vector3.Distance(_initPosition, transform.position) <= 5)
            {              
                StartCoroutine(Wander());
            }
        }
        enemyAnimator.SetFloat(_speedHash,navMeshAgent.velocity.magnitude);

    }

    private IEnumerator Wander()
    {
            yield return new WaitForSeconds(5);
            var randomPosition = Random.insideUnitSphere * 5;
            randomPosition += _initPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, 5, NavMesh.AllAreas);
            navMeshAgent.SetDestination(hit.position);           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("Get Hit");
        }
    }
}
