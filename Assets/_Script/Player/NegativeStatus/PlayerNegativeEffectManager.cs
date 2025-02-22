using NUnit.Framework.Internal.Commands;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNegativeEffectManager : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAnimManager playerAnimManager;
    [SerializeField] private CinemachineFollow cameraFollow;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Transform statusSpawnLocation;

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
        status.GetComponent<FloatingStatus>().SetCamera(cameraFollow);
    }

    
    
    private IEnumerator Stunning()
    {
        _isStun = true;
        _StunText();
        playerManager.setIsStunning();        
        yield return new WaitForSeconds(_stunDuration);
        playerManager.setStunToNormal();
        _isStun = false;
    }
    public bool IsStunActive()
    {
        return _stunDuration > 0;
    }
    #endregion

    #region BurnEffect
    private float _burnDuration;
    [SerializeField] private Transform _fireSpawnLocation;
    [SerializeField] private GameObject _fireStatus;
    [SerializeField] private GameObject _burnPrefab;
    private bool _isBurn;    
    private float _burnInterval = 1f;
    private float _burnDamage;   

    private void _BurnText()
    {
        var status = Instantiate(_fireStatus, statusSpawnLocation.position, Quaternion.identity, statusSpawnLocation);
        status.GetComponent<FloatingStatus>().SetDestroyTime(_burnDuration);
        status.GetComponent<FloatingStatus>().SetCamera(cameraFollow);
    }

    public void ApplyBurnEffect(float duration)
    {
        if (!_isBurn) 
        {            
            _burnDuration = duration;            
            StartCoroutine(Burning());
            
        }
    }
    private void _BurnEffect()
    {
        var burningPrefab = Instantiate(_burnPrefab, _fireSpawnLocation.position, Quaternion.identity, _fireSpawnLocation);
        
        burningPrefab.GetComponent<BurningEffect>().SetBurnEffectTime(_burnDuration);        
    }

    private IEnumerator Burning()
    {
        _isBurn = true;        
        _BurnEffect();        
        _BurnText();       
        float elapsed = 0f;
        while (elapsed < _burnDuration) 
        {
            _burnDamage = playerHealth.GetMaxtHealth() * Random.Range(0.5f, 1f) / 100f;
            playerHealth.TakeDamage(_burnDamage);            
            playerManager.SetIsHit();
            yield return new WaitForSeconds(_burnInterval);
            elapsed += _burnInterval;
        }
        _isBurn = false;
    }
    #endregion
}
