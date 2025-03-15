using System.Collections;
using UnityEngine;

public class MeteorRainManager : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 20f;
    [SerializeField] private ParticleSystem _particleSystem;
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
        StartCoroutine(StopParticle());
    }

    private IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(_lifeTime -1.5f);
        _particleSystem.Stop(true,ParticleSystemStopBehavior.StopEmitting);
    }
}
