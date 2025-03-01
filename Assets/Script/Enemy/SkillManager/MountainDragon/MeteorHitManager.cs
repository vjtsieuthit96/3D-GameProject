using UnityEngine;

public class MeteorHitManager : MonoBehaviour
{
    [SerializeField] private EnemyDamageManager md_EnemyDamageManager;
    [SerializeField] private float _damageMultiply = 1.5f;
    [SerializeField] private float _burnDuration = 4f;
    private void Start()
    {
        md_EnemyDamageManager = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            Health playerHealth = other.GetComponent<Health>();
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            if (playerHealth != null && playerManager != null && playerNegativeEffectManager != null)
            {
                playerHealth.TakeDamage(md_EnemyDamageManager.NormalDamage() * _damageMultiply);
                playerManager.SetIsHit();
                var rate = Random.Range(0, 100);
                if (rate <= 25)
                {
                    playerNegativeEffectManager.ApplyBurnEffect(_burnDuration);
                }
            }
        }

    }
}
