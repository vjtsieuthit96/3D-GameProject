using UnityEngine;

public class MountainDragonManager : MonsterManager
{
    [SerializeField] private MD_SkillManager _SkillManager;
    private int _attackHash;
    
    protected override void Start()
    {
        base.Start();
        _attackHash = Animator.StringToHash("attack");
    }
    protected override void Update()
    {

        base.Update();

        if (distanceAtk <= attackRange * 0.8f)
        {
            if (!monsterAnimator.GetBool("isFlying"))
            {
                LookAtTarget();
                _SkillManager.TryCastFireBall();
            }
        }
        if (distanceAtk <= attackRange * 0.75f && distanceAtk >= navMeshAgent.stoppingDistance * 1.25f)
        {
            LookAtTarget();
            _SkillManager.TryCastSpreadFire();
        }
        if (distanceAtk < navMeshAgent.stoppingDistance)
        {
            LookAtTarget();
            _SkillManager.TryCastClawCombo();
            monsterAnimator.SetTrigger(_attackHash);
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
   
    private void _StopMovement()
    {
        navMeshAgent.isStopped = true;
    }
    public void stopMovement() => _StopMovement();
    private void _ResumeMovement()
    {
        navMeshAgent.isStopped=false;
    }
    public void resumeMovement() => _ResumeMovement();  
}



