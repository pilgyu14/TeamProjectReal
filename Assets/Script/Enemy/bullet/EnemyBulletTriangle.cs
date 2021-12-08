using System.Collections;
using UnityEngine;


public class EnemyBulletTriangle : MonoBehaviour
{
    public float speed = 3f;


    void check()
    {
        if (transform.position.x > GameManager.Instance.maxPos.x + 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.x < GameManager.Instance.minPos.x - 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.y > GameManager.Instance.maxPos.y + 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);
        if (transform.position.y < GameManager.Instance.minPos.y - 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, gameObject);

    }
    private void Update()
    {
        check();
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }
}
