using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject prefabs;
    
    // index cua animation
    private int _attackHash;
    
    void Start()
    {
        _attackHash = Animator.StringToHash("Attack");
        
    }

    // Update is called once per frame
    void Update()
    {
        // nhấn phím để tấn công
        // Chỉ cho phép khi nhân vật không có attack animation khác
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAnimator.SetTrigger(_attackHash);            
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(prefabs,transform.position, Quaternion.identity);
        }
    }

    

}
