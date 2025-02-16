using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cameraPerlin;

    public void StartShake(float duration, float amplitude, float frequency)
    {
        StartCoroutine(ShakeCamera(duration, amplitude, frequency));
    }

    private IEnumerator ShakeCamera(float duration,float amplitude, float frequency)
    {
        _cameraPerlin.AmplitudeGain = amplitude;
        _cameraPerlin.FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        _cameraPerlin.AmplitudeGain = 0;
        _cameraPerlin.FrequencyGain = 0;
    }
}
