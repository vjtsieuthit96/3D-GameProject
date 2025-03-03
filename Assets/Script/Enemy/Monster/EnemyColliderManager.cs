using System.Collections;
using UnityEngine;

public class EnemyColliderManager : MonoBehaviour
{
    [SerializeField] protected BodyType _bodyType;
    [SerializeField] protected MonsterManager _monsterManager;   
    private bool _isTriggerHandled = false;
    private static bool _isAnyColliderHandle = false;
    public enum BodyType
    {
        Front,
        Back
    }  

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            if (_isTriggerHandled || _isAnyColliderHandle)
            {
                return;
            }
            
            if (!_monsterManager.GetBoolDeadHash())
            {
                Debug.Log("Enemy Is Hit");
                getHit();
                _monsterManager.ShowFloatingDamage(Random.Range(15, 55));
                _monsterManager.countGetHit();
                if (_monsterManager.IsDead())
                {
                    _monsterManager.Die();
                }
                _isTriggerHandled=true;
                _isAnyColliderHandle=true;
            }
        }
    }

    protected virtual void OnTriggerExit (Collider other)
    {
        StartCoroutine(ResetTriggerHandleState());     
    }
    private IEnumerator ResetTriggerHandleState()
    {
        yield return new WaitForSeconds(1f);
        _isTriggerHandled = false;
        _isAnyColliderHandle = false;
    }

    protected virtual void getHit()
    {        
        if (!_monsterManager.GetBoolImmuneToGetHit())
        {
            if (!_monsterManager.IsFlying())
            {
                if (_bodyType == BodyType.Front)
                {
                    _monsterManager.SetGetHit(_monsterManager.getHitHashType1());                  
                }
                else
                {
                    _monsterManager.SetGetHit(_monsterManager.getHitHashType2());                    
                }
            }
            else
            {
                _monsterManager.SetGetHit(_monsterManager.getHitHashFlying());
                _monsterManager.CountToFall();
            }
            _monsterManager.SetGetHitCount(0);
        }
    }      
}
