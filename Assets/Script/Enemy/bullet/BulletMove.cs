using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        CheckLimit();
    }

    private void CheckLimit()
    {
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > 20f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }
}
