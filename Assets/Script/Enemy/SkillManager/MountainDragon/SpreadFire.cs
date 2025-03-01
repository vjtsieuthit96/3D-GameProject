using System.Collections;
using UnityEngine;

public class SpreadFire : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _lifeTime = 3.5f;
    [SerializeField] private ParticleSystem[] _particleSystems;
    [SerializeField] private float _burnDuration = 10f;
    public float BurnDuration() => _burnDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _lifeTime);
        StartCoroutine(StopAfterDelay(_lifeTime - 1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float Damage() => _damage;

    private IEnumerator StopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < _particleSystems.Length; i++)
        {
            _particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
