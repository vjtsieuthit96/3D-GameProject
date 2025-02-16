using UnityEngine;

public class FireBallExplosionManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource.PlayOneShot(_explosionClip);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
