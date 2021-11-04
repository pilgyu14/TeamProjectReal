using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightVertical : MonoBehaviour
{
    float speed = 3f;
    [SerializeField]
    private int dir = 1;
    void Start()
    {

    }

    void Update()
    {

        if (transform.position.y < 3.5)
        {
            transform.Translate(new Vector2(1f * dir, 0f).normalized * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector2(0f, -1f).normalized * speed * Time.deltaTime);
        }
    }
}
