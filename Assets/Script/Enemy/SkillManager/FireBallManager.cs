using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosionEffect;
    private CameraShakeManager _shakeManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _fireBallSound;    
    

    private void Start()
    {        
        Invoke("DestroyFireBall", 3f);
        _shakeManager = GameObject.Find("SoulEater").GetComponent<CameraShakeManager>();
        _audioSource.PlayOneShot(_fireBallSound);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();

            if (playerManager != null && playerHealth != null)
            {
                _shakeManager.StartShake(1f, 25, 10);
                playerManager.SetIsHit();
                playerHealth.TakeDamage(damage);
                SpawnExplosionEffect();
                DestroyFireBall();
                StopSound();   
            }           
        }
        if (other.CompareTag(Constans.TERRAIN_Tag))
        {
            _shakeManager.StartShake(1f, 25, 10);
            SpawnExplosionEffect();
            DestroyFireBall();
            StopSound();
        }
    }

    private void SpawnExplosionEffect()
    {
        Instantiate (explosionEffect,transform.position,transform.rotation);
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
