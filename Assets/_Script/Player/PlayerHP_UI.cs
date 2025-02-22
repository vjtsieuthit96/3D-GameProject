using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHP_UI : MonoBehaviour
{   
   
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image background;
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color fullHealthTextColor = Color.black;
    [SerializeField] private Color midHealthColor = new Color(1f, 0.64f, 0f);
    [SerializeField] private Color midHealthTextColor = Color.black;
    [SerializeField] private Color lowHealthColor = Color.red;
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private Color backgroundNormalColor = Color.white;
    private bool isBlinking = false;  
    

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetMaxtHealth();
        UpdateHealthBar();
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
        hpText.text = healthSlider.value.ToString() + "/" + playerHealth.GetMaxtHealth();
        UpdateHealthBar();
    }  
    
    void UpdateHealthBar()
    {
        float currentHealth = playerHealth.GetCurrentHealth();
        float maxHealth = playerHealth.GetMaxtHealth();

        if (currentHealth >= maxHealth * 0.5f)
        {
            fillImage.color = fullHealthColor;
            hpText.color = fullHealthTextColor;
            background.color = backgroundNormalColor;
            StopBlinking();
        }
        else if (currentHealth >= maxHealth * 0.2f)
        {
            fillImage.color = midHealthColor;
            hpText.color = midHealthTextColor;
            background.color = backgroundNormalColor;
            StopBlinking();
        }
        else
        {
            fillImage.color = lowHealthColor;
            if (!isBlinking)
            {
                StartCoroutine(Blink());
            }
        }
    }
    void StopBlinking()
    {
        if (isBlinking)
        {
            StopCoroutine(Blink());
            fillImage.color = lowHealthColor;
            background.color = backgroundNormalColor;
            hpText.color = lowHealthColor;
            hpText.fontMaterial.DisableKeyword("GLOW_ON");
            fillImage.material.DisableKeyword("GLOW_ON");
            background.material.DisableKeyword("GLOW_ON");
            isBlinking = false;
        }
    }

    IEnumerator Blink()
    {
        isBlinking = true;
        hpText.fontMaterial.EnableKeyword("GLOW_ON");
        fillImage.material.EnableKeyword("GLOW_ON");
        background.material.EnableKeyword("GLOW_ON");

        while (playerHealth.GetCurrentHealth() < playerHealth.GetMaxtHealth() * 0.2f)
        {
            fillImage.color = lowHealthColor;
            background.color = lowHealthColor;
            hpText.color = lowHealthColor;
            yield return new WaitForSeconds(blinkDuration);
            fillImage.color = new Color(0, 0, 0, 0);
            background.color = backgroundNormalColor;
            hpText.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(blinkDuration);
        }

        isBlinking = false;
        fillImage.color = lowHealthColor;
        background.color = backgroundNormalColor;
        hpText.color = lowHealthColor;
        hpText.fontMaterial.DisableKeyword("GLOW_ON");
        fillImage.material.DisableKeyword("GLOW_ON");
        background.material.DisableKeyword("GLOW_ON");
    } 
    
}
