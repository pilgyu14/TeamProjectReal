using System.Collections;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private int barrierCount = 3;
    public GameObject[] barrier;
    public GameObject[] barrierUI;
    private SpriteRenderer playerSprite;
    private SpriteRenderer[] childSprite;

    private float currentDelay = 0f;
    private float maxDelay = 0.3f;  //공격속도
    public int bulletcnt = 1; // 총알을 몇 개 발사할 것인가 / 
    bool isDamaged = true;
    private float h, v;


    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        childSprite = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        CheckPos();
        Move();
        Reload();
        Fire();
    }
    void CheckPos()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, GameManager.Instance.minPos.x, GameManager.Instance.maxPos.x),
                                                              Mathf.Clamp(transform.position.y, GameManager.Instance.minPos.y, GameManager.Instance.maxPos.y));
    }
    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(h, v, 0);

        transform.position += moveDir.normalized * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }


    }

    void Fire()
    {
        if (currentDelay < maxDelay)
            return;
        if (Input.GetKey(KeyCode.Space))
        {
            if (bulletcnt == 1)
            {
                GameObject bullet;
                bullet = ObjectPool.Instance.GetObject(PoolObjectType.playerBullet);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;
                bullet.transform.SetParent(null);
            }
            else if (bulletcnt == 2)
            {
                GameObject bulletL;
                GameObject bulletR;
                bulletL = ObjectPool.Instance.GetObject(PoolObjectType.playerBullet);
                bulletR = ObjectPool.Instance.GetObject(PoolObjectType.playerBullet);
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                bulletL.transform.rotation = Quaternion.identity;
                bulletR.transform.rotation = Quaternion.identity;
                bulletL.transform.SetParent(null);
                bulletR.transform.SetParent(null);
            }
        }
        currentDelay = 0f;
    }

    void Reload()
    {
        currentDelay += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged == false) return;
        if (collision.CompareTag("playerBullet")) return;
        else if (collision.CompareTag("bullet_Type0"))
        {
            barrierDestroy();
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, collision.gameObject);

        }
        else if (collision.CompareTag("bullet_Type1"))
        {
            barrierDestroy();
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, collision.gameObject);

        }
    }

    public void barrierDestroy() //충돌시 배리어 까이는 거
    {

        if (barrierCount == 0)
        {
            //애니메이션 플레이 
            //씬 넘어감 
            return;
        }
        barrier[barrierCount - 1].SetActive(false);
        barrierUI[barrierCount - 1].SetActive(false);
        barrierCount--;
        StartCoroutine(SetColor());
    }

    IEnumerator SetColor() //충돌시 투명도 왔다갔다
    {
        isDamaged = false;
        Color color = playerSprite.color;
        float delay = 0.3f;
        int count = 2;


        for (int i = 0; i < count; i++)
        {
            color.a = 0.5f;
            playerSprite.color = color;
            childColorSet(count, color);
            yield return new WaitForSeconds(delay);
            color.a = 1;
            playerSprite.color = color;
            childColorSet(count, color);
            yield return new WaitForSeconds(delay);
        }
        isDamaged = true;
    }

    public void childColorSet(int count, Color color)
    {
        for (int i = 0; i < count; i++)
        {
            childSprite[i].color = color;
        }
    }
}
