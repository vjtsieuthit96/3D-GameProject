using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SoulEaterUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health dragonHealth;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = dragonHealth.GetMaxtHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = dragonHealth.GetCurrentHealth();

        //xoay slider
        healthSlider.transform.rotation = _cinemachineFollow.transform.rotation;
    }
}
