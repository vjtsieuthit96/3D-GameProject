using System.Collections;
using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager; 
    [SerializeField] private EnemyDamageManager soulEaterDamageManager;
    [SerializeField] private SoulEaterSkillManager soulEaterSkillManager;
    [SerializeField] private CameraShakeManager cameraShakeManager;    

    //Sound
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource flyAudioSource;
    [SerializeField] private AudioClip roarClip;
    [SerializeField] private AudioClip tailWhipClip;
    [SerializeField] private AudioClip biteClip;
    [SerializeField] private AudioClip getHitClip;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip flyClip;
    [SerializeField] private AudioClip landClip;

    private void Start()
    {
        SetUpAudioSource(audioSource);
        SetUpAudioSource(flyAudioSource);
    }
    private void SetUpAudioSource (AudioSource audioSource)
    {
        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 3.0f;
        audioSource.maxDistance = 70.0f;
    }
    #region AnimationEvent
    public void RoarStart()
    {
        cameraShakeManager.StartShake(2f, 15, 15);
        audioSource.PlayOneShot(roarClip); 
    }

    public void DragonNormalAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage());
            playerManager.SetIsHit();           
        }
        audioSource.PlayOneShot(biteClip);
        // rung camera
        cameraShakeManager.StartShake(0.5f, 2, 2);
    }   

    public void DragonTailAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage());
            playerManager.SetIsHit();            
        }
        audioSource.PlayOneShot(tailWhipClip);
        cameraShakeManager.StartShake(0.5f, 2, 2);
    }   

    public void FireBallStart()
    {
        soulEaterSkillManager.FireBall();
    }

    public void FlyFireBallStart()
    {
        soulEaterSkillManager.FireBall();
    }

    public void GetHitStart()
    {
        audioSource.PlayOneShot(getHitClip);
    }

    public void Die()
    {
        audioSource.PlayOneShot(dieClip);
    }

    public void TakeOff()
    {
        flyAudioSource.clip = flyClip;        
        flyAudioSource.Play();
    }

    public void Land()
    {
        flyAudioSource.Stop();       
        audioSource.PlayOneShot(landClip);
    }
    #endregion

   
}
