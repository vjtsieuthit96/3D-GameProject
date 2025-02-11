using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponCollider;

    void Start()
    {
        weaponCollider.enabled = false;
    }
    public void OnAttackStart()
    {
        Debug.Log("AttackBegin");
        weaponCollider.enabled = true;
    }

    public void OnAttackEnd() 
    {
        Debug.Log("AttackEnd");
        weaponCollider.enabled = false;
    }

}
