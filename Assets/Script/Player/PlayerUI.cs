using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMP_Text hpText;
     
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxtHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();    
        hpText.text = healthSlider.value.ToString() +"/"+ playerHealth.GetMaxtHealth();
    }
}
