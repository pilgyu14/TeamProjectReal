using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBoss : MonoBehaviour
{
    [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    private Vector2 moveDir;
    bool moveCheck = false; 

    //체력 관리 
    [SerializeField]
    private GameObject enemyCanvasGo;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        CheckPos();
    }
    void Onhit(int dmg)
    {
        hp -= dmg;
        SetColor(); 
        if(hp <= 0)
        {
            //ObjectPool.Instance.ReturnObject()
            enemyCanvasGo.SetActive(false);
            enemyCanvasGo.GetComponent<EnemyHPBar>().Init();
        }

    }
    void CheckPos()
    {
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, GameManager.Instance.minPos.x, GameManager.Instance.maxPos.x),
          //                                                    Mathf.Clamp(transform.position.y, GameManager.Instance.minPos.y, GameManager.Instance.maxPos.y));
    }

    private void Move()
    {
        if (moveCheck == false)
        {
            moveDir = Vector2.right;
            moveCheck = true; 
        }
        transform.Translate(moveDir * speed * Time.deltaTime);
        if (transform.position.x >= GameManager.Instance.maxPos.x )
        {
            Debug.Log("왼쪽");
            moveDir = Vector2.left;
        }

        if (transform.position.x <= GameManager.Instance.minPos.x)
        {
            Debug.Log("오른쪽");
            moveDir = Vector2.right; 
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("보스 ");
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            PlayerBullet playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
            Onhit(playerBullet.dmg);
            enemyCanvasGo.GetComponent<EnemyHPBar>().Damaged(hp);
            ObjectPool.Instance.ReturnObject(PoolObjectType.playerBullet, collision.gameObject);
            
        }
    }
    IEnumerator SetColor()
    {
        float delay = 0.2f; 
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        spriteRenderer.color = color;  
    }
}
