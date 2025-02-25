using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float healingRate; 
    public float _currentHealth;
    private bool _isHit;
    
    void Start()
    {
        _currentHealth = maxHealth;
        StartCoroutine(Healing());
    }
    private void Update()
    {
        _isHit = false;
    }

    public float GetCurrentHealth() => _currentHealth;
    public float GetMaxtHealth() => maxHealth;
    public void SetHealingRate(float rate) => healingRate = rate;
    public void TakeDamage (float damage)
    {
        if (!_isHit)
        {
            _currentHealth -= damage;
            _isHit = true;
        }
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
