using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private Transform targetPortal;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constans.PLAYER_TAG))
        {            
            other.transform.position = targetPortal.position;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {          
            other.transform.position = targetPortal.position;
        }
    }
}
