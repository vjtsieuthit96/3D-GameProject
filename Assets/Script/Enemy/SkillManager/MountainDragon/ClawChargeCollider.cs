using UnityEngine;

public class ClawChargeCollider : MonoBehaviour
{
    [SerializeField] private GameObject _clawAtkEffect;
    [SerializeField] private EnemyDamageManager _enemyDamageManager;
    [SerializeField] private ClawChargeManager _clawChageManager;

    private void Start()
    {
        _enemyDamageManager = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();
        _clawChageManager = GetComponentInParent<ClawChargeManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            Health playerHealth = other.GetComponent<Health>();
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            Instantiate(_clawAtkEffect, transform.position, Quaternion.identity);
            if (playerHealth != null && playerManager != null && playerNegativeEffectManager != null)
            {
                playerHealth.TakeDamage(_enemyDamageManager.NormalDamage()*_clawChageManager.DamageMultiply());
                playerManager.SetIsHit();
                var rate = Random.Range(1, 100);
                if (rate < 25)
                {
                    playerNegativeEffectManager.ApplyBurnEffect(_clawChageManager.burnDuration());
                }
            }
        }
    }
}
