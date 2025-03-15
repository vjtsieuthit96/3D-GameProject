using StarterAssets;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] StarterAssetsInputs playerInput;
    private int _attackIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackIndex = Animator.StringToHash(Constans.ATTACK);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
