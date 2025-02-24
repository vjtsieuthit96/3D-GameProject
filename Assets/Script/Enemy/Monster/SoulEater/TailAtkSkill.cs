using UnityEngine;

public class TailAtkSkill : MonoBehaviour
{
    [SerializeField] private GameObject tailAtkEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.PLAYER_TAG)) 
        {
            Instantiate(tailAtkEffect,transform.position, Quaternion.identity);
        }

    }
}
