using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;  
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private CinemachineCamera _cinemachineFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        Floating();
        if (_cinemachineFollow != null)
            transform.rotation = _cinemachineFollow.transform.rotation;
    }

    public void SetCamera(CinemachineCamera cinemachineFollow)
    {
        _cinemachineFollow = cinemachineFollow;
    }
    public void Floating()
    {
        transform.position += new Vector3(Random.Range(-1f,1f), Random.Range(1f,2f),0)*speed*Time.deltaTime;
        
    }
    public void SetText(float text)
    {
        if (text > 45)
        {
            textMesh.color = Color.red;
            textMesh.fontStyle = FontStyles.Bold;
            textMesh.fontSize = 0.75f;
            
        }

        else
        {
            textMesh.color = new Color32(255, 185, 80, 255);
            textMesh.fontSize = 0.4f;
            textMesh.fontStyle = FontStyles.Normal;
        }
        textMesh.text = text.ToString();
    }
}
