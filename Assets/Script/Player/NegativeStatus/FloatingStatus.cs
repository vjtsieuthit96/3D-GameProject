using Unity.Cinemachine;
using UnityEngine;

public class FloatingStatus : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cinemachineFollow;
    private float _destroyTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
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
        _destroyTime = destroyTime + 0.75f;
    }

    public void SetCamera (CinemachineCamera cinemachineFollow)
    {
        _cinemachineFollow = cinemachineFollow;
    }

}
