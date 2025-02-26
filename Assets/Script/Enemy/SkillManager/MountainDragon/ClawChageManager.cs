using UnityEngine;

public class ClawChageManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _tailChargeClip;
    [SerializeField] private GameObject _clawAtkEffect;
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

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            Instantiate(_clawAtkEffect,transform.position, Quaternion.identity);
        }
    }
}
