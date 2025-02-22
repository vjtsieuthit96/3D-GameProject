using UnityEngine;

public class AvatarCameraManager : MonoBehaviour
{
    public Transform nose;
    public Camera faceCamera;
    [SerializeField] private float distanceFromFace = 0.5f; // Khoảng cách từ camera đến mặt
    [SerializeField] private float verticalOffset = 2f; // Điều chỉnh cao độ của camera
    [SerializeField] private float horizontalOffset = 2.5f; // Điều chỉnh ngang của camera
    [SerializeField] private float depthOffset = -10.0f; // Điều chỉnh trục Z của camera
    [SerializeField] private float fieldOfView = 4f; // Góc nhìn của camera

    void LateUpdate() // Sử dụng LateUpdate để đảm bảo camera cập nhật sau khi nhân vật di chuyển
    {
        Vector3 faceCenter = CalculateFaceCenter();
        Vector3 offset = -nose.forward * distanceFromFace
                         + Vector3.up * verticalOffset
                         + nose.right * horizontalOffset
                         + nose.up * depthOffset; // Điều chỉnh cao độ, khoảng cách, hướng ngang và trục Z của camera

        faceCamera.transform.position = faceCenter + offset; // Đặt camera tại vị trí offset
        faceCamera.transform.LookAt(faceCenter); // Camera hướng về trung tâm khuôn mặt

        faceCamera.fieldOfView = fieldOfView; // Điều chỉnh góc nhìn của camera
    }

    Vector3 CalculateFaceCenter()
    {
        Vector3 center = nose.position;
        return center;
    }
}
