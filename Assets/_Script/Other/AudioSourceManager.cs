using UnityEngine;

public class AudioSourceManager
{    
    public static void SetUpAudioSource(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.Log("audio source is null");
            return;
        }
        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 3.0f;
        audioSource.maxDistance = 70.0f;
    }
}
