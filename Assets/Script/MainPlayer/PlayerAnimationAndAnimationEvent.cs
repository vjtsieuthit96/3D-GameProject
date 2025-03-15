using UnityEngine;

public class PlayerAnimationAndAnimationEvent : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponCollider;
    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private Transform normalHitEffectSpawnPoint;
    //[SerializeField] private PlayerManager playerManager;

    private bool _isStaying;
    public bool IsStaying => _isStaying;



    void Start()
    {
        weaponCollider.enabled = false;
    }
    public void OnAttackStart()
    {
        weaponCollider.enabled = true;
        //GameObject normalHitParticle = Instantiate(normalHitEffect, normalHitEffectSpawnPoint.position, Quaternion.identity);
    }

    public void OnAttackEnd()
    {
        weaponCollider.enabled = false;
        //GameObject normalHitParticle = Instantiate(normalHitEffect, normalHitEffectSpawnPoint.position, Quaternion.identity);
    }
    public void KnockDown()
    {
        _isStaying = true;
    }
    public void NeedGetUp()
    {
       // playerManager.SetGetUp();
    }
    public void GetUp()
    {
        _isStaying = false;
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        //if (animationEvent.animatorClipInfo.weight > 0.5f)
        //{
        //    if (FootstepAudioClips.Length > 0)
        //    {
        //        var index = Random.Range(0, FootstepAudioClips.Length);
        //        AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
        //    }
        //}
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        //if (animationEvent.animatorClipInfo.weight > 0.5f)
        //{
        //    AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        //}
    }

}
