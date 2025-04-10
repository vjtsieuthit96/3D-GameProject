using System.Collections;
using TMPro.Examples;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] private float _damageMultiply = 2f;
    [SerializeField] private GameObject explosionEffect;
    private CameraShakeManager _shakeManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fireBallSound;
    [SerializeField] private GameObject _burnAOE;
    [SerializeField] private EnemyDamageManager _enemyDamageManager;
    

    private void Start()
    {
        AudioSourceManager.SetUpAudioSource(_audioSource);
        Invoke("DestroyFireBall", 7f);
        _shakeManager = GameObject.Find("DragonBoss").GetComponent<CameraShakeManager>();
        _audioSource.PlayOneShot(_fireBallSound);
        _enemyDamageManager = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {
                _shakeManager.StartShake(1f, 15, 3);
                playerManager.SetIsHit();
                playerHealth.TakeDamage(_enemyDamageManager.NormalDamage()*_damageMultiply);
                SpawnExplosionEffect();            
            }           
        }
        if (other.CompareTag(Constans.TERRAIN_Tag))
        {
            _shakeManager.StartShake(1f, 15, 3);
            SpawnExplosionEffect();
            SpawnFlameAOE();
            DestroyFireBall();
            StopSound();
        }
    }

    private void SpawnExplosionEffect()
    {
        Instantiate (explosionEffect,transform.position,transform.rotation);
    }
    
    private void SpawnFlameAOE()
    {
        Instantiate(_burnAOE, transform.position, _burnAOE.transform.rotation);
    }

    private void DestroyFireBall()
    {
        Destroy(gameObject);
    }

    private void StopSound()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }        
    }
}
