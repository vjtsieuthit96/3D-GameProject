using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField] private float normalDamage;   

    public float NormalDamage() => normalDamage;
    
}
