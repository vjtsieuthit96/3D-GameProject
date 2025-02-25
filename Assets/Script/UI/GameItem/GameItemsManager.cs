using UnityEngine;

public class GameItemsManager : MonoBehaviour
{
    [SerializeField] private int _id;
    public int ID => _id;
    [SerializeField] public string _itemName;
    [SerializeField] public Sprite _icon;
    [SerializeField] public float _value;
    [SerializeField] public Rigidbody rgbody;
    [SerializeField] public BoxCollider boxCollider;

    void Start()
    {      
        rgbody.AddForce(Vector3.up * 10f + Vector3.right * Random.Range(-1f, 1f), ForceMode.Impulse);       
        
    } 

    void Update()
    {
        
    }   
}
