using System.Runtime.CompilerServices;
using UnityEngine;

public class HornSkillManager : MonoBehaviour
{
    
    [SerializeField] private NightMareSkillManager nightMareSkillManager;
    [SerializeField] private GameObject electroHitPrefab;
    [SerializeField] private PlayerNegativeEffectManager negativeEffectManager;
    [SerializeField] private Transform player;
    [SerializeField] private Transform knockbackSource;
    [SerializeField] private float knockbackForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {
                playerManager.SetKnockback();
                playerHealth.TakeDamage(nightMareSkillManager.ClawDamage);
                //KnockBack
                Vector3 knockbackDirection = (player.transform.position - knockbackSource.position).normalized;
                
                negativeEffectManager.ApplyKnockBack(knockbackDirection,knockbackForce);                
            }
            SpawnHitEffect();
        }

        if (other.CompareTag(Constans.TERRAIN_Tag))
        {
            SpawnHitEffect();
        }
    }

    private void SpawnHitEffect()
    {
        Instantiate(electroHitPrefab, transform.position, transform.rotation);
    }
}
