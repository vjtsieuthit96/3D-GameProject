using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class MountainDragonManager : MonsterManager
{
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private MD_SkillManager _SkillManager;
    private int _attackHash;
    [SerializeField] private float _flyingTime = 120f;
    
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
            _capsuleCollider.enabled = true;
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
            if (distanceAtk > navMeshAgent.stoppingDistance && distanceAtk <= navMeshAgent.stoppingDistance + 2)
            {
                _SkillManager.TryCastJumpSkill();
            }
            if (distanceAtk < navMeshAgent.stoppingDistance)
            {              
                _SkillManager.TryCastClawCombo();
                monsterAnimator.SetTrigger(_attackHash);
            }
        }
        else
        {
            _capsuleCollider.enabled = false;
            _SkillManager.TryCastMeteorRain();
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
        _attackTime = 0;
    }

    protected override float SetFlyTime() => _flyingTime;   

    private void _AdjustHeightFly()
    {
        StartCoroutine(AdjustHeightOverTime(_flyHeight, 2.5f));
    }
    public void AdjustHeightFly()=>_AdjustHeightFly();
    private void _AdjustHeightLand(float duration)
    {
       StartCoroutine(AdjustHeightOverTime(0f, duration));
    }
    public void AdjustHeightLand(float duration)=>_AdjustHeightLand(duration);
    
    public void StopMovement() => base._StopMovement();    
    public void ResumeMovement() => base._ResumeMovement();   
  
}



