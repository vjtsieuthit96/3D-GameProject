using UnityEngine;

public class NightMareAnimationManager : MonoBehaviour
{
    [SerializeField] private NightMareSkillManager nightMareSkillManaer;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    public void Jump()
    {
        nightMareSkillManaer.JumpSkill();
    }
}
