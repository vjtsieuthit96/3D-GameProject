using UnityEngine;

public class LightningClawSkill : MonoBehaviour
{
    [SerializeField] private NightMareSkillManager nightMareSkillManager;
    [SerializeField] private GameObject electroHitPrefab;
    [SerializeField] private PlayerNegativeEffectManager negativeEffectManager;
    [SerializeField] private float _stunDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {                
                playerManager.SetIsHit();
                playerHealth.TakeDamage(nightMareSkillManager.ClawDamage);
                var rate = Random.Range(1, 100);
                if (rate <= 100)
                {
                    negativeEffectManager.ApplyStunEffect(_stunDuration);
                    SpawnHitEffect();
                }
            }
        }
    }

    private void SpawnHitEffect()
    {
        Instantiate(electroHitPrefab, transform.position, transform.rotation);
    }
}
