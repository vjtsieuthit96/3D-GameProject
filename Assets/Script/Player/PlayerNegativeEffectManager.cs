using System.Collections;
using Unity.Cinemachine;
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
    private float _knockbackForce;
    private float _knockbackDuration = 0.5f;

    private float _knockbackTimer;
    private Vector3 _knockbackDirection;

    public void ApplyKnockBack(Vector3 direction, float knockbackForce)
    {
        _knockbackForce = knockbackForce;
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
    public void UpdateKnockBackMovement() => _UpdateKnockBackMovement();

    public bool IsKnockBackActive()
    {
        return _knockbackTimer > 0;
    }
    #endregion

    #region isStunEffect

    private float _stunDuration;
    [SerializeField] private GameObject stunStatus;
    [SerializeField] private Transform statusSpawnLocation;
    [SerializeField] private CinemachineFollow cameraFollow;
    private bool _isStun = false;
    public void ApplyStunEffect(float duration)
    {
        if (!_isStun)
        {
            _stunDuration = duration;
            StartCoroutine(Stunning());
        }

    }

    private void _StunText()
    {        
        var status = Instantiate(stunStatus, statusSpawnLocation.position, Quaternion.identity, statusSpawnLocation);
        status.GetComponent<FloatingStatus>().SetDestroyTime(_stunDuration);
    }
    
    private IEnumerator Stunning()
    {
        _isStun = true;
        playerManager.setIsStunning();
        _StunText();
        yield return new WaitForSeconds(_stunDuration);
        playerManager.setStunToNormal();
        _isStun = false;
    }
    public bool IsStunActive()
    {
        return _stunDuration > 0;
    }
    #endregion
}
