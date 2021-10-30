using System.Collections;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject bullet; 
    private float h, v; 
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(h,v,0);
        //transform.Translate(moveDir.normalized * speed * Time.deltaTime);

        transform.position += moveDir.normalized * speed * Time.deltaTime;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.8f , 3.15f), Mathf.Clamp(transform.position.y, -4.9f, 4.9f));
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2f ; 
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.playerBullet);
            bullet.transform.position = transform.position;
            bullet.transform.SetParent(null);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.tag == "playerBullet") return;
        else if (collision.tag == "bullet_Type0")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, collision.gameObject);
        else if (collision.tag == "bullet_Type1")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, collision.gameObject);
    }

}
