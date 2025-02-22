using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private CinemachineBasicMultiChannelPerlin _cameraPerlin;
    [SerializeField] private Transform _shakeSource;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _maxDistance = 60f;   

    public void StartShake(float duration, float maxamplitude, float frequency)
    {
        float distance = Vector3.Distance(_shakeSource.position, _camera.position);
        float shakeamplitude = Mathf.Lerp(0.5f,maxamplitude, 1 - (distance/_maxDistance));
        StartCoroutine(ShakeCamera(duration, shakeamplitude, frequency));
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
