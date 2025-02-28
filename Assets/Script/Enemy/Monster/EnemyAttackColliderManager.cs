using UnityEngine;

public class EnemyAttackColliderManager : MonoBehaviour
{
    [SerializeField] private EnemyDamageManager _enemyDamage;
    [SerializeField] private float _damageMultiply = 1f;
    private void Start()
    {
        _enemyDamage = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            Debug.Log("Player is Hit");
            Health playerHealth = other.GetComponent<Health>();
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerHealth != null && playerManager != null)
            {
                playerHealth.TakeDamage(_enemyDamage.NormalDamage()*_damageMultiply);
                playerManager.SetIsHit();
            }
        }
    }
}
