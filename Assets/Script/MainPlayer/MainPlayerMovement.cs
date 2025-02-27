using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = transform.right * horizontal + transform.forward * vertical;

        characterController.Move(move * speed * Time.deltaTime);

        if(move != Vector3.zero)
        {
            playerPrefab.transform.rotation = Quaternion.LookRotation(move * Time.deltaTime);
        }
    }
}
