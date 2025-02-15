using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager; 
    [SerializeField] private EnemyDamageManager soulEaterDamageManager;
    [SerializeField] private SoulEaterSkillManager soulEaterSkillManager;


    public void DragonNormalAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage());
            playerManager.SetIsHit();           
        }
    }   

    public void DragonTailAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(soulEaterDamageManager.NormalDamage());
            playerManager.SetIsHit();            
        }
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
