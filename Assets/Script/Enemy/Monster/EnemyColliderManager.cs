using UnityEngine;

public class EnemyColliderManager : MonoBehaviour
{
    [SerializeField] protected BodyType _bodyType;
    [SerializeField] protected MonsterManager _monsterManager;   
    private bool _isTriggerHandled = false; 
    public enum BodyType
    {
        Front,
        Back
    }  

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.WEAPON_TAG))
        {
            if (_isTriggerHandled)
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
            }
        }
    }

    protected virtual void OnTriggerExit (Collider other)
    {
        _isTriggerHandled = false;       
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
            }
            _monsterManager.SetGetHitCount(0);
        }
    }      
}
