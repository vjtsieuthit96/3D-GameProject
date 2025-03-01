using System.Collections;
using UnityEngine;

public class FlySpreadFire : MonoBehaviour

{
    [SerializeField] private ParticleSystem[] _particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3f);
        StartCoroutine(StopPartilce(1.5f));
    }

    private IEnumerator StopPartilce(float aftertime)
    {
        yield return new WaitForSeconds (aftertime);
        for (int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        } 
    }
   
}
