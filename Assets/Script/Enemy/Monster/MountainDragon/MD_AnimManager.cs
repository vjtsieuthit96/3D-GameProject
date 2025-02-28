using UnityEngine;

public class MD_AnimManager : MonoBehaviour
{
    [SerializeField] private MD_SkillManager _skillManager;
    [SerializeField] private MountainDragonManager _mountainDragonManager;

    //FireBreath Skill
    #region FireBreath
    private void FireBreathStart()
    {
        _mountainDragonManager.StopMovement();        
    }

    private void FireBreath()
    {
        _skillManager.SpreadFire();
    }

    private void FireBreathEnd()
    {
        _mountainDragonManager.ResumeMovement();
    }
    #endregion

    //Fly & Land
    #region Fly&Land
    private void Fly()
    {
        _mountainDragonManager.AdjustHeightFly();
    }

    private void StationaryLand()
    {
        _mountainDragonManager.AdjustHeightLand();
    }

    private void GlideLand()
    {
        _mountainDragonManager.AdjustHeightLand();
    }
    #endregion

    //ClawCombo
    #region ClawCombo
    private void ClawComboStart()
    {
        _mountainDragonManager.StopMovement();
    }

    private void RightClawStart()
    {
        _skillManager.ClawRightCombo();
    }

    private void LeftClawStart()
    {
        _skillManager.ClawLeftCombo();
    }

    private void ClawComboEnd()
    {
        _mountainDragonManager.ResumeMovement();
    }
    #endregion

    //FireBall
    #region FireBall
    private void GroundFireBallStart()
    {
        _mountainDragonManager.StopMovement();
        _skillManager.FireBall();
    }

    private void GroundFireBallEnd()
    {
        _mountainDragonManager.ResumeMovement();
    }
    #endregion

    //Roar
    #region Roar
    private void RoarStart()
    {
        _mountainDragonManager.LookTarget();
        _mountainDragonManager.StopMovement();
    }

    private void RoarEnd()
    {
        _mountainDragonManager.NormalLook();
        _mountainDragonManager.ResumeMovement();
    }
    #endregion

    //FlySpreadFire
    #region FlySpreadFire   
    private void FlySpreadFireStart()
    {
        _mountainDragonManager.StopMovement();
    }

    private void FlySpreadFire()
    {
        _skillManager.FlySpreadFire();
        StartCoroutine(_skillManager.FireTornado(3));
    }

    private void FlySpreadFireEnd()
    {
        _mountainDragonManager.ResumeMovement();
        StopCoroutine(_skillManager.FireTornado(3));
    }
    #endregion

    //FlyFireBall
    #region FlyFireBall
    private void FlyFireBallStart()
    {
        _mountainDragonManager.StopMovement();
        _skillManager.FireBall();
    }

    private void FlyFireBallEnd()
    {
        _mountainDragonManager.ResumeMovement();
    }
    #endregion
}
