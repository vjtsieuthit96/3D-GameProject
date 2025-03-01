using System.Collections;
using UnityEngine;

public class Firebreath : MonoBehaviour
{    
    [SerializeField] private SpreadFire _spreadFire;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Start()
    {
        _spreadFire = GetComponentInParent<SpreadFire>();
        StartCoroutine(StopAfterDelay(_spreadFire.lifeTime - 1.5f));
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

    private IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _particleSystem.Stop(true,ParticleSystemStopBehavior.StopEmitting);
    }
}
