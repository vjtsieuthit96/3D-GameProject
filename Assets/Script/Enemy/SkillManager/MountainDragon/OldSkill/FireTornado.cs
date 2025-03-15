using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireTornado : MonoBehaviour
{
    #region Fields
    [SerializeField] private float lifeTime = 15f;
    [SerializeField] private EnemyDamageManager _enemyDamageManager;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float initialRadius = 1f;
    [SerializeField] private float radiusGrowthRate = 0.5f;
    [SerializeField] private float sinAmplitude = 1f;
    [SerializeField] private float sinFrequency = 1f;
    [SerializeField] private float zigzagAmplitude = 1f;
    [SerializeField] private float zigzagFrequency = 1f;   
    #endregion


    private Vector3 centerPoint;
    private float angle;
    private float radius;

    void Start()
    {
        _enemyDamageManager = GameObject.Find("DragonBoss").GetComponent<EnemyDamageManager>();
        centerPoint = transform.position;
        angle = 0f;
        radius = initialRadius;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Tăng góc theo thời gian để tạo chuyển động tròn
        angle += moveSpeed * Time.deltaTime;

        // Tăng bán kính theo thời gian để tạo chuyển động xoắn ốc
        radius += radiusGrowthRate * Time.deltaTime;

        // Tính toán vị trí mới dựa trên góc, bán kính, chuyển động hình sin và chuyển động zic zac
        float x = Mathf.Cos(angle) * radius; // Chuyển động tròn theo trục x
        float z = Mathf.Sin(angle) * radius + Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude; // Chuyển động tròn và zic zac theo trục z
        float y = Mathf.Sin(Time.time * sinFrequency) * sinAmplitude; // Chuyển động hình sin theo trục y

        transform.position = centerPoint + new Vector3(x, y, z); // Cập nhật vị trí mới
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            Health playerHealth = other.GetComponent<Health>();
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();
            if (playerManager != null && playerHealth != null && playerNegativeEffectManager != null)
            {
                playerHealth.TakeDamage(_enemyDamageManager.NormalDamage());
                playerManager.SetIsHit();
                playerNegativeEffectManager.ApplyBurnEffect(5f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {         
            PlayerNegativeEffectManager playerNegativeEffectManager = other.GetComponent<PlayerNegativeEffectManager>();            
            if (playerNegativeEffectManager != null)
            {
                playerNegativeEffectManager.ApplyBurnEffect(5f);  
            }
        }
    }
     

}
