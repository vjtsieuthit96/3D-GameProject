using System.Collections;
using Unity.Cinemachine;
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
        if (!_isFlying)
        {
            if (distanceAtk <= attackRange * 0.6f)
            {
                if (!monsterAnimator.GetBool(_isFlyingHash))
                {               
                    _SkillManager.TryCastFireBall();                
                }
            }
            if (distanceAtk <= attackRange * 0.75f && distanceAtk >= navMeshAgent.stoppingDistance * 1.25f)
            {         
                _SkillManager.TryCastSpreadFire();         
            }
            if (distanceAtk < navMeshAgent.stoppingDistance)
            {              
                _SkillManager.TryCastClawCombo();
                monsterAnimator.SetTrigger(_attackHash);
            }
        }
        else
        {
            _SkillManager.TryCastFireBall();            
        }

    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void LookAtTarget()
    {
        base.LookAtTarget();
    }

    public void LookTarget() => LookAtTarget();
    protected override void BackToNormalLook()
    {
        base.BackToNormalLook();
    }

    public void NormalLook() => BackToNormalLook();

    protected override IEnumerator Flying()
    {
        navMeshAgent.speed += 4;
        navMeshAgent.height = _flyHeight;
        _SkillManager.SetFireBallCD(-50);
        yield return base.Flying();
        _SkillManager.SetFireBallCD(50);
        navMeshAgent.speed -= 4;
        navMeshAgent.height = 4;
    }   

    private void _AdjustHeightFly()
    {
        StartCoroutine(AdjustHeightOverTime(_flyHeight, 2.5f));
    }
    public void AdjustHeightFly()=>_AdjustHeightFly();
    private void _AdjustHeightLand()
    {
       StartCoroutine(AdjustHeightOverTime(0f, 2.5f));
    }
    public void AdjustHeightLand()=>_AdjustHeightLand();

    protected override void _StopMovement()
    {
        base._StopMovement();
    }   
    public void StopMovement() => _StopMovement();
    override protected void _ResumeMovement()
    {
        base._ResumeMovement();
    }
    public void ResumeMovement() => _ResumeMovement();  
}



