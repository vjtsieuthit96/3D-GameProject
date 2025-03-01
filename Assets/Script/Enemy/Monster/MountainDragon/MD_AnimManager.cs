using UnityEngine;

public class MD_AnimManager : MonoBehaviour
{
    [SerializeField] private MD_SkillManager _skillManager;
    [SerializeField] private MountainDragonManager _mountainDragonManager;
    [SerializeField] private SphereCollider _lHandCollider;
    [SerializeField] private SphereCollider _rHandCollider;
    [SerializeField] private SphereCollider _toothCollider;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioClip getHitClip;
    [SerializeField] private AudioClip roarClip;
    [SerializeField] private AudioClip spreadFire;
    [SerializeField] private AudioClip flyingClip;
    [SerializeField] private AudioClip takeoffClip;
    [SerializeField] private AudioClip landClip;
    [SerializeField] private AudioClip glideClip;
    [SerializeField] private AudioClip flyStationClip;
    [SerializeField] private AudioClip jumpBiteClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip clawClip;

    private Coroutine _meteorRainCoroutine;

    private void Start()
    {
        _lHandCollider.enabled = false;
        _rHandCollider.enabled = false;
        _toothCollider.enabled = false;
        AudioSourceManager.SetUpAudioSource(audioSource);  
        AudioSourceManager.SetUpAudioSource(walkAudioSource);
        walkAudioSource.clip = walkClip;
    }
    private void WalkStart()
    {
        walkAudioSource.Play();
    }
    private void WalkEnd()
    {
        walkAudioSource.Stop();
    }

    private void RunStart()
    {
        audioSource.PlayOneShot(runClip);
    }
    private void RunEnd()
    {
        //
    }

    //FireBreath Skill
    #region FireBreath
    private void FireBreathStart()
    {
        _mountainDragonManager.StopMovement();        
    }

    private void FireBreath()
    {
        _skillManager.SpreadFire();
        audioSource.PlayOneShot(spreadFire); 
    }

    private void FireBreathEnd()
    {
        _mountainDragonManager.ResumeMovement();
    }
    #endregion

    //Fly & Land
    #region Fly&Land
    private void FlyStationStart()
    {
        audioSource.PlayOneShot(flyStationClip);
    }
    private void FlyStationEnd()
    {
        //
    }
    private void GlideStart()
    {
        audioSource.PlayOneShot(glideClip);
    }
    private void GlideEnd()
    {
       //
    }
    private void Flying()
    {
        audioSource.PlayOneShot(flyingClip);
    }
    private void FlyingEnd()
    {
        //
    }
    private void Fly()
    {
        audioSource.PlayOneShot(takeoffClip);
        _mountainDragonManager.AdjustHeightFly();
    }

    private void StationaryLand()
    {
        _mountainDragonManager.AdjustHeightLand();
        audioSource.PlayOneShot(landClip);
    }

    private void GlideLand()
    {
        _mountainDragonManager.AdjustHeightLand();
        audioSource.PlayOneShot(landClip);
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
        audioSource.PlayOneShot(roarClip);
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
       _meteorRainCoroutine = StartCoroutine(_skillManager.MeteorRain(3));
        audioSource.PlayOneShot(spreadFire);
    }

    private void FlySpreadFireEnd()
    {
        _mountainDragonManager.ResumeMovement();
        StopCoroutine(_meteorRainCoroutine);
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

    //NormalAttack
    #region NormalAttack
    private void ClawsLeftAttackStart()
    {
        audioSource.PlayOneShot(clawClip);
        _lHandCollider.enabled = true;
    }
    private void ClawsLeftAttackEnd() 
    { 
        _lHandCollider.enabled = false;
    }
    private void ClawsRightAttackStart()
    {
        audioSource.PlayOneShot(clawClip);
        _rHandCollider.enabled = true;
    }
    private void ClawsRightAttackEnd()
    {
        _rHandCollider.enabled = false;
    }
    private void ClawsAttackLeftForwardStart()
    {
        audioSource.PlayOneShot(clawClip);
        _lHandCollider.enabled = true;
    }
    private void ClawsAttackLeftForwardEnd()
    {
        _lHandCollider.enabled= false;
    }
    private void ClawsAttackRightForwardStart()
    {
        audioSource.PlayOneShot(clawClip);
        _rHandCollider.enabled = true;
    }
    private void ClawsAttackRightForwardEnd()
    {
        _rHandCollider.enabled= false;
    }
    private void JumpBiteStart()
    {
        audioSource.PlayOneShot(jumpBiteClip);
        _toothCollider.enabled = true;
    }
    private void JumpBiteEnd()
    {
        _toothCollider.enabled = false;
    }

    #endregion

    private void GetHitFrontStart()
    {
        audioSource.PlayOneShot(getHitClip);
    }
    private void GetHitBackStart()
    {
        audioSource.PlayOneShot(getHitClip);
    }
    private void FlyGetHitStart()
    {
        audioSource.PlayOneShot(getHitClip);
    }
    private void GlideGetHitStart()
    {
        audioSource.PlayOneShot(getHitClip);
    }
}
