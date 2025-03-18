using System.Collections;
using UnityEngine;

public class PlayerAnimationAndAnimationEvent : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponCollider;
    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private Transform normalHitEffectSpawnPoint;
    [SerializeField] private Transform TopOfBlade;

    //Skill
    [SerializeField] private GameObject skill1Effect;
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
        //
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
    private void OnNormalHit()
    {
        GameObject normalHitParticle = Instantiate(normalHitEffect, normalHitEffectSpawnPoint.position, Quaternion.identity);
    }
    private void OnSkill1Effect()
    {
        GameObject skill1Particle = Instantiate(skill1Effect, TopOfBlade.position, TopOfBlade.rotation);
       
        MoveParticle(skill1Particle);
        Destroy(skill1Particle, 2.5f);
    }
    IEnumerator MoveParticle(GameObject particle)
    {
        float elapsedTime = 0f;
        float duration = 1f;
        Vector3 startPosition = particle.transform.position;
        Vector3 targetPosition = startPosition + Vector3.forward * 5f;

        while (elapsedTime < duration)
        {
            particle.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        particle.transform.position = targetPosition;
    }
}
