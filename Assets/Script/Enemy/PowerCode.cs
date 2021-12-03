using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCode : MonoBehaviour
{
    [SerializeField]
    float angle = 30;
    [SerializeField]
    float maxTime = 1; 

    private BasicBoss basicBoss;

    bool isDirChange = false;
    float time = 0f;
    float startAngle= 20f, endAngle= 100f; 

    private bool IsHard = false;
    public bool IsTime = true; 
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        StartCoroutine(ChangeDir());
    }
    private void Start()
    {
        basicBoss = FindObjectOfType<BasicBoss>(); 
    }
    void Update()
    {
        DIrChange();
        if (IsTime)
        {
            CheckTime();
        }
            angle = Mathf.Lerp(startAngle, endAngle, time * 0.15f);
       
    }

    private void CheckTime()
    {
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            IsTime = false;
            basicBoss.MoveToCenter();
            //basicBoss.IsBack = true;
            //basicBoss.think(); 
            //gameObject.SetActive(false);
            angle = 30f; 
            time = 0f;
        }
    }
    private void DIrChange()
    {
        if(isDirChange==false)
            transform.Rotate(new Vector3(0, 0, angle) * Time.deltaTime);
        else
            transform.Rotate(new Vector3(0, 0, -angle) * Time.deltaTime);
    }
  
    IEnumerator ChangeDir()
    {
        float delay = 1f; 
        isDirChange = false; 
        yield return new WaitForSeconds(delay);
        isDirChange = true;
        yield return new WaitForSeconds(delay);
    }

}
