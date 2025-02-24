using UnityEngine;
using UnityEngine.UIElements;

public class MonsterDeadManager : MonoBehaviour
{
    [SerializeField] private Health monsterHealth;
    [SerializeField] private GameObject[] itemDrop;
    [SerializeField] private Transform dropPosition;
    [SerializeField] private CharacterController monsterController;
    [SerializeField] private SphereCollider monsterCollider;
    
    private bool _hasDroppedItem = false;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
       if (MonsterIsDead() && !_hasDroppedItem)
        {
            DropItem();
            monsterController.enabled = false;
            monsterCollider.enabled = false;
            _hasDroppedItem=true;
        }
    }

    private bool MonsterIsDead()
    {
        return (monsterHealth.GetCurrentHealth() <= 0);
    }

    private void DropItem()
    {
        var random = Random.Range(0, 100);
        if (random < 90)
        {
            var sword = Instantiate(itemDrop[0], dropPosition.position + new Vector3 (0,4f,0), Quaternion.identity);
        }
        else
        {
            var bazoka = Instantiate(itemDrop[1], dropPosition.position + new Vector3(0, 4f, 0), Quaternion.identity);
        }
        
    }

    
}
