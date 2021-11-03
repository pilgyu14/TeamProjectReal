using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D rigid; 
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        rigid.AddForce(Vector3.forward* 10);

    }
}
