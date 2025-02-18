using Unity.VisualScripting;
using UnityEngine;

public class PlayerNegativeEffectManager : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAnimManager playerAnimManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region KnockBackEffect
    [SerializeField] private float _knockbackForce = 10.0f;
    [SerializeField] private float _knockbackDuration = 0.5f;
    
    private float _knockbackTimer;
    private Vector3 _knockbackDirection;

    public void ApplyKnockBack(Vector3 direction)
    {
        _knockbackDirection = direction.normalized;
        _knockbackTimer = _knockbackDuration;        
    }

    private void _UpdateKnockBackMovement()
    {
        if (_knockbackTimer > 0)
        {
            characterController.Move(_knockbackDirection * _knockbackForce * Time.deltaTime);        
            _knockbackTimer -= Time.deltaTime;
        }
    }
    public void UpdateKnockBackMovement() =>_UpdateKnockBackMovement();

    public bool IsKnockBackActive()
    {
        return _knockbackTimer > 0;
    }
    #endregion
}
