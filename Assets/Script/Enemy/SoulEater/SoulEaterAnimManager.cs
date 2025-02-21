using System.Collections;
using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager; 
    [SerializeField] private EnemyDamageManager soulEaterDamageManager;
    [SerializeField] private SoulEaterSkillManager soulEaterSkillManager;
    [SerializeField] private CameraShakeManager cameraShakeManager;
    [SerializeField] private SphereCollider tailSphere;
    [SerializeField] private SoulEaterManager soulEaterManager;
    private bool _isFlying;
    public bool isFlying() => _isFlying;

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
        AudioSourceManager.SetUpAudioSource(audioSource);      
        AudioSourceManager.SetUpAudioSource(flyAudioSource);
        tailSphere.enabled = false;
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

    public void TailAtkStart()
    {
        soulEaterSkillManager.TailCharge();
    }

    public void DragonTailAtk()
    {
        tailSphere.enabled = true;
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage()*1.5f);            
            playerManager.SetIsHit();
            var rate = Random.Range(0f, 100f);
            if (rate < 15.0f)
            {
                soulEaterSkillManager.ApplyTailBurnEffect();               
            }             
        }
        audioSource.PlayOneShot(tailWhipClip);
        cameraShakeManager.StartShake(0.5f, 2, 2);
    }   
    public void TailAtkEnd()
    {
        tailSphere.enabled = false;
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
        soulEaterManager.AdjustHeight();
        flyAudioSource.clip = flyClip;        
        flyAudioSource.Play();
        _isFlying = true;
    }

    public void Land()
    {
        soulEaterManager.Land();
        flyAudioSource.Stop();       
        audioSource.PlayOneShot(landClip);        
    }

    public void CompleteLand()
    {
        _isFlying = false;
    }
    #endregion

   
}
