using UnityEngine;

public class LigtningClawSkill : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private SphereCollider _sphereCollider;    

    private void Start()
    {
        _sphereCollider.enabled = true;
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
                playerHealth.TakeDamage(_damage);              
            }
        }
    }  

}
