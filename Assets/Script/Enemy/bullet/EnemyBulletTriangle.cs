using System.Collections;
using UnityEngine;


public class EnemyBulletTriangle : MonoBehaviour
{
    public float speed = 3f;

    private void Start()
    {
    }

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
    private void Update()
    {
        check();
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
}


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.tag == "player")
//            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);


//    }
//}