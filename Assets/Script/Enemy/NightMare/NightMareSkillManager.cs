using System.Collections;
using UnityEngine;

public class NightMareSkillManager : MonoBehaviour
{
    [SerializeField] private Animator nightMareAnimator;
    [SerializeField] private Transform target;
    
    private int _clawSkillHash;
    private int _hornSkillHash;
    private int _defendSkillHash;
    private int _jumpSkillHash;


    private void Start()
    {
        _defendSkillHash = Animator.StringToHash("defend");
        _jumpSkillHash = Animator.StringToHash("jump");
        _clawSkillHash = Animator.StringToHash("clawSkill");
        _hornSkillHash = Animator.StringToHash("hornSkill");
    }
    #region ClawSkill
    [SerializeField] private float _clawSkillCD = 10f;
    [SerializeField] private float _clawDamage = 30.0f;
    public float ClawDamage => _clawDamage;
    [SerializeField] private GameObject clawSkillPrefab;
    [SerializeField] private Transform[] clawSkillSpawnPoint;
    
    private bool _canCastClawSkill = true;

    public void TryCastClawSkill()
    {
        if (_canCastClawSkill)
        {
            StartCoroutine(CastClawSkill());
        }
    }

    private IEnumerator CastClawSkill()
    {
        _canCastClawSkill = false;
        nightMareAnimator.SetTrigger(_clawSkillHash);
        yield return new WaitForSeconds(_clawSkillCD);
        _canCastClawSkill = true;
    }

    private void _ClawSkill()
    {
        foreach (Transform spawnpoint in clawSkillSpawnPoint)
        Instantiate(clawSkillPrefab, spawnpoint.position, Quaternion.identity,spawnpoint);
    }

    public void ClawSkill() => _ClawSkill();
    #endregion

    #region HornSkill


    [SerializeField] private float _hornSkillCD = 1f;
    [SerializeField] private float _hornDamage = 30.0f;
    public float HornDamage => _hornDamage;
    [SerializeField] private GameObject hornSkillPrefab;
    [SerializeField] private Transform hornSkillSpawnPoint;
    private bool _canCastHornSkill = true;

    public void TryCastHornSkill()
    {
        if (_canCastHornSkill)
        {
            StartCoroutine(CastHornSkill());
        }
    }

    private IEnumerator CastHornSkill()
    {
        _canCastHornSkill = false;
        nightMareAnimator.SetTrigger(_hornSkillHash);
        yield return new WaitForSeconds(_hornSkillCD);
        _canCastHornSkill = true;
    }

    private void _HornSkill()
    {
        Instantiate(hornSkillPrefab, hornSkillSpawnPoint.position, Quaternion.identity, hornSkillSpawnPoint);
    }
    public void HornSkill() => _HornSkill();
    #endregion

    #region JumpSkill
    [SerializeField] private float _jumpSkillCD = 15.0f;    
    [SerializeField] private CharacterController nightmareController;
    [SerializeField] private float _jumpDistance = 20.0f;
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
        nightMareAnimator.SetTrigger(_jumpSkillHash);       
        yield return new WaitForSeconds(_jumpSkillCD);
        _canCastJumpSkill = true;
    }   
   

    private void _JumpSkill()
    {         
        StartCoroutine(MoveTowardJumpSkill());        
    }


    public void JumpSkill()=>_JumpSkill();

    private IEnumerator MoveTowardJumpSkill()
    {
        Vector3 startPosition = nightmareController.transform.position;
        Vector3 direction = (target.position - startPosition).normalized;
        Vector3 targetPosition = startPosition + direction*_jumpDistance;

        float elapsedTime = 0f;
        float jumpDuration = 1.25f;
        while (elapsedTime < jumpDuration)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime/jumpDuration);
            newPosition.y = nightmareController.transform.position.y;
            nightmareController.Move(newPosition - nightmareController.transform.position);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }        
    }

    #endregion


}
