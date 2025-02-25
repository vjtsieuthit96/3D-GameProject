using System.Collections;
using UnityEngine;

public class MD_SkillManager : MonoBehaviour
{
    [SerializeField] private Animator _mdAnimator;
    [SerializeField] private Transform target;
    [SerializeField] private PlayerNegativeEffectManager _negativeEffectManager;
    [SerializeField] private MountainDragonManager _mdManager;
    private int _fireballSkillHash;
    private int _clawComboHash;
    private int _spreadFireSkillHash;


    private void Start()
    {
        _fireballSkillHash = Animator.StringToHash("fireballSkill");
        _clawComboHash = Animator.StringToHash("clawCombo");
        _spreadFireSkillHash = Animator.StringToHash("spreadFireSkill");
    }
    #region FireBallSkill


    [SerializeField] private float fireBallCD = 10f;

    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private float fireBallBurnDuration = 10f;
    [SerializeField] private float fireBallSpeed = 5f;
    private bool _canCastFireBall = true;
    public void TryCastFireBall()
    {
        if (_canCastFireBall)
        {            
            StartCoroutine(CastFireBall());
        }
    }

    private void _SetFireBallCD(float percent)
    {
        fireBallCD += percent / 100 * fireBallCD;
    }
    public void SetFireBallCD(float percent) => _SetFireBallCD(percent);
    private IEnumerator CastFireBall()
    {
        _canCastFireBall = false; ;
        _mdAnimator.SetTrigger(_fireballSkillHash);
        yield return new WaitForSeconds(fireBallCD);
        _canCastFireBall = true;
    }

    private void _FireBall()
    {
        GameObject fireball = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().linearVelocity = (target.position - fireBallSpawnPoint.position).normalized * fireBallSpeed;
    }
    public void FireBall() => _FireBall();
    private void _ApplyFireBallBurn()
    {
        _negativeEffectManager.ApplyBurnEffect(fireBallBurnDuration);
    }
    public void ApplyFireBallBurn()=>_ApplyFireBallBurn();
    #endregion

    #region ClawCombo
    [SerializeField] private GameObject _clawCombo;
    [SerializeField] private Transform[] _clawComboRightSpawnLocation;
    [SerializeField] private Transform[] _clawComboLeftSpawnLocation;
    [SerializeField] private float _clawComboBurnDuration = 3f;
    [SerializeField] private float _clawComboCD = 10f;
    private bool _canCastClawCombo = true;

    public void TryCastClawCombo()
    {
        if (_canCastClawCombo)
        {
            StartCoroutine(CastClawCombo());
        }
    }
    private IEnumerator CastClawCombo()
    {
        _canCastClawCombo = false; 
        _mdAnimator.SetTrigger(_clawComboHash);
        yield return new WaitForSeconds(_clawComboCD);
        _canCastClawCombo = true;
    }
    private void _ClawLeftCombo()
    {
        for (int i = 0; i < _clawComboLeftSpawnLocation.Length; i++)
        {
            Instantiate(_clawCombo, _clawComboLeftSpawnLocation[i].position, Quaternion.identity, _clawComboLeftSpawnLocation[i]);
        }
    }
    public void ClawLeftCombo() => _ClawLeftCombo();
    private void _ClawRightCombo()
    {
        for (int i = 0; i < _clawComboRightSpawnLocation.Length; i++)
        {
            Instantiate(_clawCombo, _clawComboRightSpawnLocation[i].position, Quaternion.identity, _clawComboRightSpawnLocation[i]);
        }
    }
    public void ClawRightCombo() => _ClawRightCombo();

    private void _ApplyClawComboBurn()
    {
        _negativeEffectManager.ApplyBurnEffect(_clawComboBurnDuration);
    }
    public void ApplyClawComboBurn()=>_ApplyClawComboBurn();
    #endregion
    #region SpreadFire
    [SerializeField] private GameObject _spreadFire;
    [SerializeField] private Transform _spreadFireSpawnLocation;
    [SerializeField] private float _spreadFireBurnDuration = 10f;
    [SerializeField] private float _spreadFireCD = 15f;
    private bool _canCastSpreadFire = true;
    public void TryCastSpreadFire()
    {
        if(_canCastSpreadFire)
        {
            StartCoroutine(CastSpreadFire());
        }
    }
    private IEnumerator CastSpreadFire()
    {
        _canCastSpreadFire = false;
        _mdAnimator.SetTrigger(_spreadFireSkillHash);
        yield return new WaitForSeconds(_spreadFireCD);
        _canCastSpreadFire = true;
    }
    private void _SpreadFire()
    {
        Instantiate(_spreadFire,_spreadFireSpawnLocation.position,Quaternion.identity, _spreadFireSpawnLocation);
    }
    public void SpreadFire()=>_SpreadFire();
    private void _ApplySpreadFireBurn()
    {
        _negativeEffectManager.ApplyBurnEffect(_spreadFireBurnDuration);
    }
    public void ApplySpreadFireBurn() => _ApplySpreadFireBurn();


    #endregion
}
