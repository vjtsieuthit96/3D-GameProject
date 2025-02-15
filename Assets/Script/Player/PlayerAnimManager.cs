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
        weaponCollider.enabled = true;
    }

    public void OnAttackEnd() 
    {       
        weaponCollider.enabled = false;
    }

}
