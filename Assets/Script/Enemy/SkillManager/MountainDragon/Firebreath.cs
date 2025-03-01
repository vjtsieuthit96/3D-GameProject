using System.Collections;
using UnityEngine;

public class Firebreath : MonoBehaviour
{    
    [SerializeField] private SpreadFire _spreadFire;  

    private void Start()
    {
        _spreadFire = GetComponentInParent<SpreadFire>();  
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag(Constans.PLAYER_TAG))
        {
            Health playerHealth = other.GetComponent<Health>();
            PlayerManager playerManager = other.GetComponentInParent<PlayerManager>();
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_spreadFire.Damage());
                playerManager.SetIsHit();
                playerNegativeEffectManager.ApplyBurnEffect(_spreadFire.BurnDuration());
            }
        }
    }

    
}
