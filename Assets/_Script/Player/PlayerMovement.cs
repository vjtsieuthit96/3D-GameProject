using UnityEngine;

//Named rules
// tên class/hame: PascalCase
// tên biến camelCase
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float speed = 10.0f;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private PlayerAnimManager playerAnimManager;

    [SerializeField] private PlayerNegativeEffectManager playerNegativeEffectManager;

    void Start()
    {

    }

    void Update()
    {
        // Player Dead sẽ ko di chuyển
        if (playerManager.ISDEAD() || playerAnimManager.IsStaying || playerManager.GetBoolStuning())
        {
            if (playerNegativeEffectManager.IsKnockBackActive())
            {                
                playerNegativeEffectManager.UpdateKnockBackMovement();
            }
            return;
        }        
        
            // Nhân vật di chuyển
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var move = transform.right * horizontal + transform.forward * vertical;

            characterController.Move(move * speed * Time.deltaTime);

            //Chạy annimation
            playerAnimator.SetFloat("Speed", move.magnitude);

            //Nhân vật chạm đất

            if (!characterController.isGrounded)
            {
                characterController.Move(Vector3.down * 9.8f * Time.deltaTime);
            }

            //Xoay nhân vật theo hướng di chuyển

            if (move != Vector3.zero)
            {
                playerPrefab.transform.rotation = Quaternion.LookRotation(move * Time.deltaTime);
            }       

    }
}


