using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{

    [SerializeField]
    private Transform enemy;
    [SerializeField]
    private Slider HPSlider;
    [SerializeField]
    private Slider BackHPSlider;
    private bool backHphit = false;

    public float currentHP = 100f;
    [SerializeField]
    private float MaxHP = 100f;

    void Start()
    {

    }

    void Update()
    {
        //Debug.Log(currentHP);
        //transform.position = enemy.position;
        HPSlider.value = Mathf.Lerp(HPSlider.value, currentHP / MaxHP, 5f * Time.deltaTime);
        if (backHphit)
        {
            BackHPSlider.value = Mathf.Lerp(BackHPSlider.value, HPSlider.value, 10f * Time.deltaTime);
            if (HPSlider.value >= BackHPSlider.value - 0.01f)
            {
                BackHPSlider.value = HPSlider.value;
                backHphit = false;
            }
        }
    }
    public void Damaged(float hp)
    {
        currentHP = hp;
        Invoke("BackDamaged", 0.5f);
    }
    void BackDamaged()
    {
        backHphit = true;
    }
    public void Init()
    {
        HPSlider.value = 1f;
    }
}
