using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDiagonal : MonoBehaviour
{
    float speed = 2f;
    [SerializeField]
    private int dir = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
        if(transform.position.y < 6)
        {
            transform.Translate(new Vector2(1f * dir, -1f).normalized * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(0f, -1f).normalized * speed * Time.deltaTime);
        }
    }
}
