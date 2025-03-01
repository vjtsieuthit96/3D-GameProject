using UnityEngine;

public class MeteorRainManager : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 20f;
    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
