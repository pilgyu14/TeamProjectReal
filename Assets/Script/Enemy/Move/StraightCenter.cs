using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightCenter : MonoBehaviour
{
    float speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
