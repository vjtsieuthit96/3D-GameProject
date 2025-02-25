using UnityEngine;

public class MountainDragonManager : MonsterManager
{
    [SerializeField] private MD_SkillManager _SkillManager;
    private int _attackHash;
    private Vector3 backPosition;
    private bool isMovingBack=false;
    
    protected override void Start()
    {
        base.Start();
        _attackHash = Animator.StringToHash("attack");
    }
   
    protected override void Update()
    {
        base.Update();
        if(distanceAtk<=attackRange*0.8f )
        {
            if (!monsterAnimator.GetBool("isFlying"))
            {
                LookAtTarget();
                _SkillManager.TryCastFireBall();
            }
        }
        if (distanceAtk < navMeshAgent.stoppingDistance)
        {
            LookAtTarget();
            _SkillManager.TryCastClawCombo();
            _SkillManager.TryCastSpreadFire();
            monsterAnimator.SetTrigger(_attackHash);

        }
        if(isMovingBack)
        {
            transform.position = Vector3.MoveTowards(transform.position, backPosition, Time.deltaTime * 4f);
            if(Vector3.Distance(transform.position,backPosition)<0.5f)
            {
                isMovingBack = false;
            }
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }    
       
    private void _AdjustHeightFly()
    {
        StartCoroutine(AdjustHeightOverTime(_flyHeight, 1f));
    }
    public void AdjustHeightFly()=>_AdjustHeightFly();
    private void _AdjustHeightLand()
    {
       StartCoroutine(AdjustHeightOverTime(0f, 1f));
    }
    public void AdjustHeightLand()=>_AdjustHeightLand();
}



