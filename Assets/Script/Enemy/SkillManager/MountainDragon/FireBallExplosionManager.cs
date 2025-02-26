using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallExplosionManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionClip;
    [SerializeField] private float _damageMultiply = 1.5f;
    [SerializeField] private float _burnDuration;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private EnemyDamageManager _enemyDamageManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSourceManager.SetUpAudioSource(_audioSource);
        _audioSource.PlayOneShot(_explosionClip);  
        _collider.enabled = true;
        StartCoroutine(turnoffCollider());
        _enemyDamageManager = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();

            if (playerManager != null && playerHealth != null)
            {
                playerManager.SetIsHit();
                playerHealth.TakeDamage(_enemyDamageManager.NormalDamage()*_damageMultiply);
                var rate = Random.Range(0f, 100f);
                {
                    if (rate < 25f)
                    {
                        playerNegativeEffectManager.ApplyBurnEffect(_burnDuration);
                    }                    
                }                
            }            
        }        
    }
    
    private IEnumerator turnoffCollider()
    {
        yield return new WaitForSeconds(0.75f);
        _collider.enabled = false;
    }


}
