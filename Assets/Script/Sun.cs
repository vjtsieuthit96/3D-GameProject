using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private Light sunLight;
    [SerializeField] private float dayIntensity = 1.0f;
    [SerializeField] private float nightIntensity = 0.1f;
    [SerializeField] private Material morningSkybox;
    [SerializeField] private Material afternoonSkybox;
    [SerializeField] private Material eveningSkybox;
    [SerializeField] private Material nightSkybox;

    void Start()
    {
        // Đặt Skybox ban đầu
        RenderSettings.skybox = morningSkybox;
    }

    void Update()
    {
        float currentTime = Time.time;
        float angle = currentTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(angle, 0, 0);

        // Thay đổi cường độ ánh sáng dựa trên góc xoay
        if (angle % 360 < 180)
        {
            sunLight.intensity = Mathf.Lerp(nightIntensity, dayIntensity, (angle % 180) / 180);
        }
        else
        {
            sunLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, (angle % 180) / 180);
        }

        // Thay đổi Skybox dựa trên góc xoay
        ChangeSkybox(angle);
    }

    void ChangeSkybox(float angle)
    {
        float normalizedAngle = angle % 360;

        if (normalizedAngle < 90)
        {
            RenderSettings.skybox = morningSkybox;
        }
        else if (normalizedAngle < 180)
        {
            RenderSettings.skybox = afternoonSkybox;
        }
        else if (normalizedAngle < 270)
        {
            RenderSettings.skybox = eveningSkybox;
        }
        else
        {
            RenderSettings.skybox = nightSkybox;
        }
    }
}
