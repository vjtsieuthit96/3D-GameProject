using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AoeFlame : MonoBehaviour
{
    [SerializeField] private float _burnDuration;
    [SerializeField] private float _burnTime = 15f;
    [SerializeField] private ParticleSystem[] _particleSystems;
    public float burnTime => _burnTime;
    
    //[SerializeField] private AudioSource _audioSource;
    //[SerializeField] private AudioClip _burningFireClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AudioSourceManager.SetUpAudioSource(_audioSource); 
        Destroy(gameObject, _burnTime);
        //_audioSource.clip = _burningFireClip;        
        //_audioSource.Play();
        StartCoroutine(StopPartilceAdterDelay(_burnTime -1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            {
                playerNegativeEffectManager.ApplyBurnEffect(_burnDuration);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            {
                playerNegativeEffectManager.ApplyBurnEffect(_burnDuration);
            }
        }
    }

    private IEnumerator StopPartilceAdterDelay(float timetostop)
    {
        yield return new WaitForSeconds(timetostop);
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            _particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

}
