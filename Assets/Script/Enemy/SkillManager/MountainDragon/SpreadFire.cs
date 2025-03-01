using UnityEngine;

public class SpreadFire : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _lifeTime = 5f;
    public float lifeTime => _lifeTime;
    [SerializeField] private float _burnDuration = 10f;
    public float BurnDuration() => _burnDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Damage() => _damage;
}
