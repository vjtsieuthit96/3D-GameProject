using UnityEngine;

public class TailChageManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tailChargeClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource.clip = _tailChargeClip;
        _audioSource.Play();
        Destroy(gameObject,1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
