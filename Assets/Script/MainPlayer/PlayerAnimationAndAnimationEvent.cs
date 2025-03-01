using UnityEngine;

public class PlayerAnimationAndAnimationEvent : MonoBehaviour
{
    [SerializeField] private BoxCollider weaponCollider;
    [SerializeField] private GameObject normalHitEffect;
    [SerializeField] private Transform normalHitEffectSpawnPoint;
    //[SerializeField] private PlayerManager playerManager;

    private bool _isStaying;
    public bool IsStaying => _isStaying;



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
    public void KnockDown()
    {
        _isStaying = true;
    }
    public void NeedGetUp()
    {
       // playerManager.SetGetUp();
    }
    public void GetUp()
    {
        _isStaying = false;
    }

}
