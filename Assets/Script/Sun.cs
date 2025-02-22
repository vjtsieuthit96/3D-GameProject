using UnityEngine;

public class Sun : MonoBehaviour
{
    // Tốc độ xoay của mặt trời
    [SerializeField] private float rotationSpeed = 10.0f;

    // Đối tượng ánh sáng mặt trời
    [SerializeField] private Light sunLight;

    // Intensity ban ngày và ban đêm của ánh sáng
    [SerializeField] private float dayIntensity = 1.0f;
    [SerializeField] private float nightIntensity = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Lấy thời gian hiện tại
        float currentTime = Time.time;

        // Tính toán góc xoay dựa trên thời gian
        float angle = currentTime * rotationSpeed;

        // Xoay ánh sáng mặt trời quanh trục X
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
    }
}
