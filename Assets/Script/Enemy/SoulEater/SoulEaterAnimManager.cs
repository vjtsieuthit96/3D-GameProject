using Unity.Cinemachine;
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
    [SerializeField] private AudioClip roarClip;
    [SerializeField] private AudioClip tailWhipClip;
    [SerializeField] private AudioClip biteClip;
    [SerializeField] private AudioClip getHitClip;
    [SerializeField] private AudioClip dieClip;


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
}
