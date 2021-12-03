using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCodeBullet : MonoBehaviour
{
    public int lineNum;
    public bool IsMove = false;
    Vector3 targetPos;
    BasicBoss basicBoss;
   
    void Start()
    {
        basicBoss = FindObjectOfType<BasicBoss>();
        targetPos = basicBoss.transform.position; 
    }

    void Update()
    {
        if (IsMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 2);
            if (transform.position == targetPos)
            {
                IsMove = false;
                //ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type2, gameObject);
            }
        }
        if (lineNum == 9 && transform.position == targetPos)
        {
            Debug.Log("しけいぉいけぉけいぞ");
            //basicBoss.think();
            transform.parent.gameObject.SetActive(false);
            basicBoss.ClearRotateBullet();
            basicBoss.temp = -1; 
        }
    }
}
