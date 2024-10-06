using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject bossHealth;
    public Image healthImg;
    private float lerpSpeed = 3;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        bossHealth.SetActive(false);
    }
    private void OnEnable()
    {
        bossHealth.SetActive(true);

        
    }

    

    void Update()
    {
        healthImg.fillAmount = Mathf.Lerp(healthImg.fillAmount,(float)Boss.CurrentHealth/(float)Boss.GetMaxHealth(),lerpSpeed*Time.deltaTime);
        
    }
}
