using System.Collections;
using UnityEngine;

public class SoulEaterSkillManager : MonoBehaviour
{
    [SerializeField] private Animator soulEaterAnimator;
    
    private void Start()
    {
        _skillHash = Animator.StringToHash("Skill");
    }
    #region FireBallSkill
        
    
    [SerializeField] private float fireBallCoolDown = 10f;

    private bool _canCastFireBall = true;
    private int _skillHash;
    public void TryCastFireBall()
    {
        if (_canCastFireBall)
        {
            StartCoroutine(CastFireBall());
        }
    }

    private IEnumerator CastFireBall()
    {
        _canCastFireBall = false;
        soulEaterAnimator.SetTrigger(_skillHash);  
        yield return new WaitForSeconds(fireBallCoolDown);
        _canCastFireBall = true;        
    }
    #endregion
    
}
