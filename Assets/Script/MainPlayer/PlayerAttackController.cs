using StarterAssets;
using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] StarterAssetsInputs playerInput;

    // Index
    private int _attackIndex;
    private int _comboAtk1Index;
    private int _comboAtk2Index;

    // Delay Time;
    [Range(1.1f,1.5f)]
    [SerializeField] float delayAttack = 1.5f; 
    private float _waitTimeAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackIndex = Animator.StringToHash(Constans.ATTACK);
        _comboAtk1Index = Animator.StringToHash(Constans.COMBO_ATTACK1);
        _comboAtk2Index = Animator.StringToHash(Constans.COMBO_ATTACK2);
    }

    // Update is called once per frame
    void Update()
    {
        // reset combo attack
        if (Time.time > _waitTimeAttack)
        {
            if(playerAnim.GetBool(_comboAtk1Index))
                playerAnim.SetBool(_comboAtk1Index, false);

            if(playerAnim.GetBool(_comboAtk2Index))
                playerAnim.SetBool(_comboAtk2Index, false);
        }

        if (playerInput.attack && Time.time > _waitTimeAttack)
        {
            playerAnim.SetTrigger(_attackIndex);
            _waitTimeAttack = Time.time + delayAttack;
            Debug.Log("Attack");
            playerInput.attack = false;
        }
        // Make combo attack when press attack button before delay time
        else if(Time.time < _waitTimeAttack)
        {
            if(playerInput.attack && playerAnim.GetBool(_comboAtk1Index))
            {
                playerAnim.SetBool(_comboAtk2Index, true);
                Debug.Log("Combo Attack2");
                playerInput.attack = false;
            }
            else
            if (playerInput.attack)
            {
                playerAnim.SetBool(_comboAtk1Index, true);
                _waitTimeAttack = Time.time + delayAttack;
                Debug.Log("Combo Attack1");
                playerInput.attack = false;
            }
          
        }
    }

}
