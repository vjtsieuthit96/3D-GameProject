using Unity.Cinemachine;
using UnityEngine;

public class FloatingStatus : MonoBehaviour
{
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    private float _destroyTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cinemachineFollow = GameObject.Find(Constans.CameraFollow_1).GetComponent<CinemachineFollow>();
        Destroy(this.gameObject,_destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_cinemachineFollow != null)
            transform.rotation = _cinemachineFollow.transform.rotation;
    }  
   
     public void SetDestroyTime(float destroyTime)
    {
        destroyTime = _destroyTime;
    }

}
