using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour
{
    void check()
    {
        if (transform.position.x > 3.2)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.x < -9.8)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.y > 5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.y < -5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);

    }


}
