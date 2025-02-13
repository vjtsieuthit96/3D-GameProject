using UnityEngine;

public class PlayerManager : MonoBehaviour
{    
    [SerializeField] private CharacterController characterController;   
    private bool _isTouching;     

    public bool GetIsTouching() => _isTouching;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.DRAGON_TAG))
        {
            _isTouching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constans.DRAGON_TAG))
        {
            _isTouching = false;
        }
    }
}
