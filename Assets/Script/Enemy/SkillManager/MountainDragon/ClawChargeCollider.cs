using UnityEngine;

public class ClawChargeCollider : MonoBehaviour
{
    [SerializeField] private GameObject _clawAtkEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG))
        {
            Instantiate(_clawAtkEffect, transform.position, Quaternion.identity);
        }
    }
}
