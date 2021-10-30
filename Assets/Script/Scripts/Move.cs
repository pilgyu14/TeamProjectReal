using System.Collections;
using UnityEngine;


public class Move : MonoBehaviour
{
    private float speed = 10f;
    private bool isCol = false;
    private Vector2 direction;
    [SerializeField]
    private Rigidbody2D rigid = null;
    void Start()
    {
        //igid = GetComponent<Rigidbody2D>();
        //moveCol();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    //private void moveCol()
    //{
    //    direction = new Vector2(0, -0.5f);
    //    direction = direction.normalized;

    //    rigid.AddForce(direction * 10f); 

    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "bullet") return;

    //    direction = new Vector2(0, 1f);
    //    Debug.Log("1" + direction); 
    //    rigid.AddForce(direction );
    //    Debug.Log("2" + direction* -1f);

    //    if (isCol == true) return;






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }

    }
}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (isCol == true) return;
    //    if (collision.tag == "player")
    //    {
    //        Destroy(gameObject);
    //    }

    //}
//}
   

