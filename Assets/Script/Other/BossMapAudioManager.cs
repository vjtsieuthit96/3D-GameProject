using UnityEngine;

public class BossMapAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private AudioClip _backgroundClip;
    
    void Start()
    {
        _backgroundMusicSource.clip = _backgroundClip;
        _backgroundMusicSource.loop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            _backgroundMusicSource.Play();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            _backgroundMusicSource.Stop();
        }
    }

}
