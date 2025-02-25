using UnityEngine;

public class Firebreath : MonoBehaviour
{
    [SerializeField] private float _damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag(Constans.PLAYER_TAG))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damage);
            }
        }
    }
}
