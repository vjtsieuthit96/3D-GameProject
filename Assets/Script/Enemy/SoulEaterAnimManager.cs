using UnityEngine;

public class SoulEaterAnimManager : MonoBehaviour
{
    
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private Transform target;
    [SerializeField] private float fireBallSpeed = 5f;


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

    public void FireBallStart()
    {
        GameObject fireball = Instantiate(FireBall, fireBallSpawnPoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().linearVelocity = (target.position - fireBallSpawnPoint.position).normalized * fireBallSpeed;
    }

    public void FlyFireBallStart()
    {
        GameObject fireball = Instantiate(FireBall, fireBallSpawnPoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().linearVelocity = (target.position - fireBallSpawnPoint.position).normalized * fireBallSpeed;
    }
}
