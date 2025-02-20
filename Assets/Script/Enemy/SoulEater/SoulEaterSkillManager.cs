using NUnit.Framework.Internal.Commands;
using System.Collections;
using UnityEngine;

public class SoulEaterSkillManager : MonoBehaviour
{
    [SerializeField] private Animator soulEaterAnimator;    
    [SerializeField] private Transform target;
    [SerializeField] private PlayerNegativeEffectManager negativeEffectManager;
    [SerializeField] private SoulEaterManager soulEaterManager;
    private int _skillHash;


    private void Start()
    {
        _skillHash = Animator.StringToHash("Skill");
    }
    #region FireBallSkill
        
    
    [SerializeField] private float fireBallCD = 10f;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private float fireBallSpeed = 5f;
    private bool _canCastFireBall = true; 
    public void TryCastFireBall()
    {
        if (_canCastFireBall)
        {
            soulEaterManager.lookAtTarget();
            StartCoroutine(CastFireBall());
        }
    }

    private IEnumerator CastFireBall()
    {
        _canCastFireBall = false;        ;
        soulEaterAnimator.SetTrigger(_skillHash);  
        yield return new WaitForSeconds(fireBallCD);
        _canCastFireBall = true;        
    }

    private void _FireBall()
    {
        GameObject fireball = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().linearVelocity = (target.position - fireBallSpawnPoint.position).normalized * fireBallSpeed;
    }
    public void FireBall() => _FireBall();
    #endregion

    #region TailAtk
    [SerializeField] private GameObject TailAtkCharge;
    [SerializeField] private Transform SpawnLocation;
    [SerializeField] private float _tailBurnEffectduration = 3f;
    private void _TailCharge()
    {
        Instantiate(TailAtkCharge,SpawnLocation.position, Quaternion.identity,SpawnLocation);
    }
    public void TailCharge()=>_TailCharge();

    public void ApplyTailBurnEffect()
    {
       negativeEffectManager.ApplyBurnEffect(_tailBurnEffectduration);       
    }
    #endregion

}
