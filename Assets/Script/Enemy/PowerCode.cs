using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCode : MonoBehaviour
{
    [SerializeField]
    float angle = 30;
    [SerializeField]
    float maxTime = 10f; 

    public BasicBoss basicBoss;

    bool isDirChange = false;
    float time = 0f;
    float startAngle= 20f, endAngle= 100f; 

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        StartCoroutine(ChangeDir());
    }
    void Update()
    {
        Move(); 
        CheckTime();
        angle = Mathf.Lerp(startAngle, endAngle, time * 0.15f);
       
    }

    private void CheckTime()
    {
        time += Time.deltaTime;
        if (time >= maxTime)
        {
            basicBoss.ClearRotateBullet();
            gameObject.SetActive(false);
            angle = 30f; 
            time = 0f;
        }
    }
    private void Move()
    {
        if(isDirChange==false)
            transform.Rotate(new Vector3(0, 0, angle) * Time.deltaTime);
        else
            transform.Rotate(new Vector3(0, 0, -angle) * Time.deltaTime);
    }
  
    IEnumerator ChangeDir()
    {
        float delay = 4f; 
        isDirChange = false; 
        yield return new WaitForSeconds(delay);
        isDirChange = true;
        yield return new WaitForSeconds(delay);
    }

}
