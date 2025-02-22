using TMPro;
using UnityEngine;
using UnityEngine.UI;

class PlayerGameItem
{
    public GameItemsManager Item;
    public int Amount;
}
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _inventoryCanvas;
    [SerializeField] private Button _itemSlotPrefabs;
    private Button[] _itemSlot = new Button[50];

    [SerializeField] PlayerGameItem[] _items = new PlayerGameItem[50];


    void Start()
    {        
        _inventoryCanvas.SetActive(false);
        _inventoryPanel.SetActive(false);
        InitialSlot();   
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventoryCanvas.SetActive(!_inventoryCanvas.activeSelf);
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
            if (_inventoryPanel.activeSelf)
            {
                DisplayInventory();
            }
        }
    }

    //Display inventory
    private void InitialSlot()
    {       

        for (int i = 0; i < 50; i++)
        {
            Button slot  = Instantiate(_itemSlotPrefabs,_inventoryPanel.transform);
            slot.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";                    
            slot.gameObject.SetActive(true);
        }
    }

    void DisplayInventory()
    {
        //for (int i = 0; i < _items.Length; i++)
        //{
        //    if (_items[i] == null) continue;
        //    {
                
        //    }
             
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            // nếu item có trong inventory thì tăng số lượng
            // nếu chưa có thì thêm vào inventory
            var item = other.gameObject.GetComponent<GameItemsManager>();
            //kiểm tra  xem item có trong inventory chưa

            var check = false;
            for (int i = 0; i < _items.Length; i++) 
            { 
                if (_items[i].Item.ID == item.ID)
                {
                    check = true;
                    _items[i].Amount++;
                    break;
                }
            }
            if (check == false)
            {
                for (int i = 0; i<_items.Length; i++)
                {

                }
            }
        }
    }
}
