using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] private int damage;

    private void Start()
    {
        Invoke("Destroy", 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {
                playerManager.SetIsHit();
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }           
        }
        if (other.CompareTag(Constans.TERRAIN_Tag))
        {
            Destroy(gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
