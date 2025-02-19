using Unity.VisualScripting;
using UnityEngine;

public class AoeFlame : MonoBehaviour
{
    [SerializeField] private float _burnDuration;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _burningFireClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSourceManager.SetUpAudioSource(_audioSource); 
        Destroy(this.gameObject, 10.0f);
        _audioSource.clip = _burningFireClip;        
        _audioSource.Play();
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
}
