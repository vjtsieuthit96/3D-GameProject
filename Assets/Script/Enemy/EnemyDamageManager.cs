using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    [SerializeField] private float normalDamage;   

    public float NormalDamage() => normalDamage;
    
}
