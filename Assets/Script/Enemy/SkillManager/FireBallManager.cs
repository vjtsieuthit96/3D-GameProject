using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] private int damage;
    private CameraShakeManager _shakeManager;

    private void Start()
    {        
        Invoke("DestroyFireBall", 3f);
        _shakeManager = GameObject.Find(Constans.CameraFollow_1).GetComponent<CameraShakeManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {
                _shakeManager.StartShake(0.5f, 3, 3);
                playerManager.SetIsHit();
                playerHealth.TakeDamage(damage);  
                DestroyFireBall();
            }           
        }
        if (other.CompareTag(Constans.TERRAIN_Tag))
        {
            _shakeManager.StartShake(0.5f, 3, 3);
            DestroyFireBall();
        }
    }

    

    private void DestroyFireBall()
    {
        Destroy(gameObject);
    }
}
