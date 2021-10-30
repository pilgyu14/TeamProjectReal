using System.Collections;
using UnityEngine;


    public class move2 : MonoBehaviour
    {
    private void Start()
    {
    }

    void check()
    {
        if (transform.position.x > 3.2)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, gameObject);
        if (transform.position.x < -9.8)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, gameObject);
        if (transform.position.y > 5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, gameObject);
        if (transform.position.y < -5.6)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, gameObject);

    }
    private void Update()
    {
        check();
     //   transform.Translate(Vector2.right  * Time.deltaTime, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }

}