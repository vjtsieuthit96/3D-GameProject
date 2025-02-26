using UnityEngine;

public class ClawExplosionManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tailExplosionClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource.PlayOneShot(_tailExplosionClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
