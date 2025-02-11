using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float healingRate; 
    public float _currentHealth;
    
    void Start()
    {
        _currentHealth = maxHealth;
        StartCoroutine(Healing());
    }

    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxtHealth() => maxHealth;
    public void SetHealingRate(float rate) => healingRate = rate;
    public void TakeDamage (float damage)
    {
        _currentHealth -= damage;
    }

   // hàm hồi máu dùng IEnumerator 
   IEnumerator Healing()
    {
        while (true)
        {
            _currentHealth += healingRate * Time.deltaTime;
            //_currentHealth = Mathf.Clamp( _currentHealth, 0, maxHealth );   
            _currentHealth = Mathf.Min(_currentHealth, maxHealth);
            yield return new WaitForSeconds(1f);
        }
    }
}
