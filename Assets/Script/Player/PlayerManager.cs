using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Player c� CharacterController
    // Qu�i c� CharacterContronller

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(Constans.DRAGON_TAG))
        {
            Debug.Log("Player Hit Dragon");
        }
    }
}
