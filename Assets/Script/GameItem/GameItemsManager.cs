using UnityEngine;

public class GameItemsManager : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private float _value;
    [SerializeField] private Rigidbody rgbody;
    [SerializeField] private BoxCollider boxCollider;

    void Start()
    {      
        rgbody.AddForce(Vector3.up * 10f + Vector3.right * Random.Range(-1f, 1f), ForceMode.Impulse);       
        
    } 

    void Update()
    {
        
    }   
}
