using UnityEngine;

public class BurningEffect : MonoBehaviour
{
    private float _destroyTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,_destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBurnEffectTime(float destroyTime)
    {
        _destroyTime = destroyTime + 0.25f;
    }
}
