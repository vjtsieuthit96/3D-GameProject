using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerMovement : MonoBehaviour
{
    // Player
    [SerializeField] private float sprintspeed = 5.0f;
    [SerializeField] private float moveSpeed;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerInput playerInput;

    //index animaton
    private int _speed;
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //float targetSpeed = playerInput.sprint ? sprintspeed : moveSpeed;
    }
}
