using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager;
  
    
   
    
    public void DragonNormalAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(10);
            playerManager.SetIsHit();           
        }
    }
    

    public void DragonTailAtk()
    {
        if (playerManager.GetIsTouching() == true)
        {
            playerHealth.TakeDamage(10);
            playerManager.SetIsHit();            
        }
    }

    

}
