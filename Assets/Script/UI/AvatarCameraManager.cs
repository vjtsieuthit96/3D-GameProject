using UnityEngine;

public class AvatarCameraManager : MonoBehaviour
{
    public Transform nose;
    public Camera faceCamera;
    public float distanceFromFace = 0.5f; // Khoảng cách từ camera đến mặt
    public float verticalOffset = 0.1f; // Điều chỉnh cao độ của camera
    public float horizontalOffset = 0.0f; // Điều chỉnh ngang của camera
    public float depthOffset = 0.0f; // Điều chỉnh trục Z của camera
    public float fieldOfView = 30.0f; // Góc nhìn của camera

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
