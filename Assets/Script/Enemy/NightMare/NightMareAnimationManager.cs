using UnityEngine;

public class NightMareAnimationManager : MonoBehaviour
{
    [SerializeField] private NightMareSkillManager nightMareSkillManager;
    [SerializeField] private SphereCollider clawCollider;
    [SerializeField] private SphereCollider hornCollider;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Health playerHealth;
    [SerializeField] private EnemyDamageManager enemyDamageManager;  

    private void Start()
    {
        clawCollider.enabled = false;
        hornCollider.enabled = false;
    }
    void Update()
    {
        
    }

    
    public void BasicAttack()
    {
       
        if (playerManager.GetIsTouching())
        {
            playerManager.SetIsHit();
            playerHealth.TakeDamage(enemyDamageManager.NormalDamage());
        }
    }
    public void Jump()
    {
        nightMareSkillManager.JumpSkill();
    }
 
    public void ClawAttack()
    {
       
        nightMareSkillManager.ClawSkill();
        clawCollider.enabled = true;
    } 

    public void ClawAttackEnd()
    {
        clawCollider.enabled = false;
    }   
  
    public void HornCharge()
    {
        nightMareSkillManager.HornSkill();
    }
    public void HornAttack()
    {
       
        hornCollider.enabled = true;
    }
    public void HornAttackEnd()
    {
        hornCollider.enabled = false;
    }
}
