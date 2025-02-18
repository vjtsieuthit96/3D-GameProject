using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class PlayerManager : MonoBehaviour
{    
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Animator playerAnimator;    
    private int _isAliveHash;
    private int _isHit;
    private int _isIummnity;
    private bool _isTouching;
    private bool _isDead;
    private int _KnockDownHash;
    private int _GetUpHash;
    public bool ISDEAD() => _isDead;
   

    public bool GetIsTouching() => _isTouching;

    private void Start()
    {
        _isAliveHash = Animator.StringToHash("isAlive");
        _isHit = Animator.StringToHash("isHit");
        _isIummnity = Animator.StringToHash("Immunity");
        _KnockDownHash = Animator.StringToHash("KnockDown");
        _GetUpHash = Animator.StringToHash("GetUp");
        playerAnimator.SetBool(_isAliveHash, true);
    }
    private void Update()
    {            
        if (playerHealth.GetCurrentHealth() <= 0)
        {
            Die();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.DRAGON_TAG))
        {
            _isTouching = true;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constans.DRAGON_TAG))
        {
            _isTouching = false;
        }
    }

    private void setIsHit()
    {
        playerAnimator.SetTrigger(_isHit);
    }    
    public void SetIsHit() => setIsHit();
    private void _setKnockback()
    {
        playerAnimator.SetTrigger(_KnockDownHash);
    }
    public void SetKnockback() => _setKnockback();

    private void _setGetUp()
    {
        playerAnimator.SetTrigger(_GetUpHash);        
    }
    public void SetGetUp() =>_setGetUp();

    private void Die()
    {
        if (playerAnimator.GetBool(_isAliveHash))
        {
            playerAnimator.SetBool(_isIummnity, true);
            playerAnimator.SetBool(_isAliveHash, false);
            _isDead = true;           
            TeleportToHaven();
            playerHealth.SetHealingRate(0f); 
        }
    }

    private void TeleportToHaven()
    {
        Vector3 havenPosition = transform.position + new Vector3(0, 50, 0);
        transform.position = havenPosition;
    }
    
}
