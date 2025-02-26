using UnityEngine;

public class MD_AnimManager : MonoBehaviour
{
    [SerializeField] private MD_SkillManager _skillManager;
    [SerializeField] private MountainDragonManager _mountainDragonManager;

    public void FireBreath ()
    {
        _skillManager.SpreadFire();
    }

    public void Fly()
    {
        _mountainDragonManager.AdjustHeightFly();
    }

    public void StationaryLand()
    {
        _mountainDragonManager.AdjustHeightLand();
    }

    public void GlideLand()
    {
        _mountainDragonManager.AdjustHeightLand();
    }

    public void RightClawStart()
    {
        _skillManager.ClawRightCombo();
    }

    public void LeftClawStart()
    {
        _skillManager.ClawLeftCombo();
    }

    public void GroundFireBall()
    {
        _skillManager.FireBall();
    }
   
}
