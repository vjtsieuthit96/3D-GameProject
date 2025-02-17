using UnityEngine;

public class NightMareAnimationManager : MonoBehaviour
{
    [SerializeField] private NightMareSkillManager nightMareSkillManager;   
    
    
    void Update()
    {
        
    }
    public void Jump()
    {
        nightMareSkillManager.JumpSkill();
    }
 
    public void ClawAttack()
    {
        nightMareSkillManager.ClawSkill();
    } 

    
  

}
