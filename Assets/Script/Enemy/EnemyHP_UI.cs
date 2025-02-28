using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP_UI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health monsterHealth;
    [SerializeField] private CinemachineCamera _cinemachineFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = monsterHealth.GetMaxtHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = monsterHealth.GetCurrentHealth();

        //xoay slider
        healthSlider.transform.rotation = _cinemachineFollow.transform.rotation;
    }
}
