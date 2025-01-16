using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private BoxCollider weaponCollider;
    // index cua animation
    private int _attackHash;
    
    void Start()
    {
        _attackHash = Animator.StringToHash("Attack");
        weaponCollider.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        // nhấn phím để tấn công
        // Chỉ cho phép khi nhân vật không có attack animation khác
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAnimator.SetTrigger(_attackHash);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(0.75f);
        weaponCollider.enabled = false;
    }

}
