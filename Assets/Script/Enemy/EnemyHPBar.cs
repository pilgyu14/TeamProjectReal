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


    public float currentHP; 
    private float MaxHP = 1000f; 
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHP);
        //transform.position = enemy.position;
        HPSlider.value = currentHP / MaxHP; 
    }
}
