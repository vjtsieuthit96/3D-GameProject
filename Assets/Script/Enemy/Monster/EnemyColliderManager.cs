using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyColliderManager : MonoBehaviour
{
    [SerializeField] protected BodyType _bodyType;
    [SerializeField] protected MonsterManager _monsterManager;
    [SerializeField] protected Animator _monsterAnimator;    
    protected int _getHitHashType1;
    protected int _getHitHashType2;
    protected int _getHitHashFlying;
    protected int _immuneToGetHitHash;
    protected int _isDeadHash;  
    private int _getHitCount;

    private bool _isHit;
    public enum BodyType
    {
        Front,
        Back
    }

    protected virtual void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        _immuneToGetHitHash = Animator.StringToHash("immuneToGetHit");
        _getHitHashType1 = Animator.StringToHash("getHitType1");
        _getHitHashType2 = Animator.StringToHash("getHitType2");
        _getHitHashFlying = Animator.StringToHash("getHitFlying");
        _isDeadHash = Animator.StringToHash("isDead");
    }

    protected virtual void Update()
    {
        _isHit = false;
    }
   

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            Debug.Log("Enemy Is Hit");
            if (!_monsterAnimator.GetBool(_isDeadHash))
            {                
                getHit();
                _monsterManager.ShowFloatingDamage(Random.Range(15, 55));
                if (_monsterManager.IsDead())
                {
                    Die();
                }
            }
        }
    }

    protected virtual void getHit()
    {
        if (!_isHit)
        {            
            if (!_monsterAnimator.GetBool(_immuneToGetHitHash))
            {
                if (!_monsterManager.IsFlying())
                {
                    if (_bodyType == BodyType.Front)
                    {
                        _monsterAnimator.SetTrigger(_getHitHashType1);
                    }
                    else
                    {
                        _monsterAnimator.SetTrigger(_getHitHashType2);
                    }
                }
                else
                {
                    _monsterAnimator.SetTrigger(_getHitHashFlying);
                }
                _getHitCount++;
                if (_getHitCount >= 30f)
                {
                    _monsterAnimator.SetBool(_immuneToGetHitHash, true);
                    StartCoroutine(RemoveImmunityAfterCD());
                }
            }
            _isHit = true;
        }
    }
    protected IEnumerator RemoveImmunityAfterCD()
    {
        yield return new WaitForSeconds(Constans.immuneDuration);
        _monsterAnimator.SetBool(_immuneToGetHitHash, false);
        _getHitCount = 0;
    }
   
    protected void Die()
    {
        _monsterAnimator.SetBool(_isDeadHash, true);       
    }
}
