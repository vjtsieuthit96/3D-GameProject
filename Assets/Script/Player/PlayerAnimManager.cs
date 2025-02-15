using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponCollider;
    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private Transform normalHitEffectSpawnPoint;

    void Start()
    {
        weaponCollider.enabled = false;        
    }
    public void OnAttackStart()
    {        
        weaponCollider.enabled = true;
        GameObject normalHitParticle = Instantiate(normalHitEffect, normalHitEffectSpawnPoint.position, Quaternion.identity); 
    }

    public void OnAttackEnd() 
    {       
        weaponCollider.enabled = false;
        GameObject normalHitParticle = Instantiate(normalHitEffect, normalHitEffectSpawnPoint.position, Quaternion.identity);
    }

}
