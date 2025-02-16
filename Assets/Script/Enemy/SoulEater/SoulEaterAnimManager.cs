using Unity.Cinemachine;
using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager; 
    [SerializeField] private EnemyDamageManager soulEaterDamageManager;
    [SerializeField] private SoulEaterSkillManager soulEaterSkillManager;
    [SerializeField] private CameraShakeManager cameraShakeManager;


    public void DragonNormalAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage());
            playerManager.SetIsHit();           
        }
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
}
