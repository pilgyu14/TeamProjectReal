using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    public int dmg = 1;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        check();
    }

    void check()
    {
        if (transform.position.x > 3.2)
            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, gameObject);
        if (transform.position.x < -9.8)
            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, gameObject);
        if (transform.position.y > 5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, gameObject);
        if (transform.position.y < -5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, gameObject);

    }
}
