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
    private int _flySpreadFireHash;
    private int _jumpSkillHash;

    private void Start()
    {
        _fireballSkillHash = Animator.StringToHash("fireballSkill");
        _clawComboHash = Animator.StringToHash("clawCombo");
        _spreadFireSkillHash = Animator.StringToHash("spreadFireSkill");
        _flySpreadFireHash = Animator.StringToHash("flySpreadFire");
        _jumpSkillHash = Animator.StringToHash("jumpBiteSkill");
    }
    #region FireBallSkill


    [SerializeField] private float fireBallCD = 10f;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform fireBallSpawnPoint;    
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
        _mdManager.LookTarget();
        _mdAnimator.SetTrigger(_fireballSkillHash);
        _mdManager.NormalLook();
        yield return new WaitForSeconds(fireBallCD);
        _canCastFireBall = true;        
    }

    private void _FireBall()
    {
        GameObject fireball = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody>().linearVelocity = (target.position - fireBallSpawnPoint.position).normalized * fireBallSpeed;
    }
    public void FireBall() => _FireBall();    
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
        _mdManager.LookTarget();
        _canCastClawCombo = false; 
        _mdAnimator.SetTrigger(_clawComboHash);
        _mdManager.NormalLook();
        yield return new WaitForSeconds(_clawComboCD);
        _canCastClawCombo = true;
       
    }
    private void _ClawLeftCombo()
    {
        for (int i = 0; i < _clawComboLeftSpawnLocation.Length; i++)
        {
            Instantiate(_clawCombo, _clawComboLeftSpawnLocation[i].position, transform.rotation, _clawComboLeftSpawnLocation[i]);
        }
    }
    public void ClawLeftCombo() => _ClawLeftCombo();
    private void _ClawRightCombo()
    {
        for (int i = 0; i < _clawComboRightSpawnLocation.Length; i++)
        {
            Instantiate(_clawCombo, _clawComboRightSpawnLocation[i].position, transform.rotation, _clawComboRightSpawnLocation[i]);
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
        _mdManager.LookTarget();
        _mdAnimator.SetTrigger(_spreadFireSkillHash);
        _mdManager.NormalLook();
        yield return new WaitForSeconds(_spreadFireCD);
        _canCastSpreadFire = true;        
    }   
   
    private void _SpreadFire()
    {
        Instantiate(_spreadFire,_spreadFireSpawnLocation.position,transform.rotation, _spreadFireSpawnLocation);
    }
    public void SpreadFire()=>_SpreadFire();
    #endregion

    #region FireTornado
    [SerializeField] private GameObject _flySpreadFire;
    [SerializeField] private GameObject _fireTornado;
    [SerializeField] private float _fireTornadoCD = 45f;
    private bool _canCastFireTornado = true;
    public void TryCastFireTornado()
    {
        if (_canCastFireTornado)
        {
            StartCoroutine(CastFireTornado());
        }
    }
    private IEnumerator CastFireTornado()
    {
        _canCastFireTornado = false;
        _mdManager.LookTarget();
        _mdAnimator.SetTrigger(_flySpreadFireHash);
        _mdManager.NormalLook();
        yield return new WaitForSeconds(_fireTornadoCD);
        _canCastFireTornado = true;
    }

    private void _FlySpreadFire()
    {
        Instantiate(_flySpreadFire, _spreadFireSpawnLocation.position,transform.rotation, _spreadFireSpawnLocation);
    }
    public void FlySpreadFire() => _FlySpreadFire();

    private IEnumerator _FireTornado(int duration)
    {
        for (int i=0;i<duration;i++)
        {
            Vector3 spawnLocation = GetRandomPositionAroundTarget();
            Instantiate(_fireTornado, spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }
    public IEnumerator FireTornado(int duration) => _FireTornado(duration);

    private Vector3 GetRandomPositionAroundTarget()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        return target.position + randomOffset;
    }
    #endregion
    #region JumpBite
    [SerializeField] private float _jumpSkillCD = 15.0f;    
    [SerializeField] private float _jumpDistance = 5f;
    private bool _canCastJumpSkill = true;

    public void TryCastJumpSkill()
    {
        if (_canCastJumpSkill)
        {
            StartCoroutine(CastJumpSkill());
        }
    }

    private IEnumerator CastJumpSkill()
    {
        _canCastJumpSkill = false;
        _mdAnimator.SetTrigger(_jumpSkillHash);
        yield return new WaitForSeconds(_jumpSkillCD);
        _canCastJumpSkill = true;
    } 
#endregion
}
