using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private Button _itemSlotPrefabs;

    private Button[] _itemSlot = new Button[50];
    void Start()
    {
        DisplayInventory();   
    }
    
    void Update()
    {
        
    }

    //Display inventory
    private void DisplayInventory()
    {
        // xóa slot cũ
        foreach (var slot in _itemSlot)
        {
            Destroy(slot);
        }

        for (int i = 0; i < 50; i++)
        {
            Button slot  = Instantiate(_itemSlotPrefabs,_inventoryPanel.transform);
            slot.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            _itemSlot[i] = slot;            
            slot.gameObject.SetActive(true);
        }
    }
}
