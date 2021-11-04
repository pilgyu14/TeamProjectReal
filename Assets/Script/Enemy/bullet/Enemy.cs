using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet = null;
    [SerializeField]
    private Transform trans;
    public Transform playerTrans;
   // public Image hpBar; 

    [SerializeField]
    private int speed = 3;
    private bool isDamaged = false; 

    //PoolManager pool = null;

    //체력 관리 
    [SerializeField]
    private GameObject enemyCanvasGo;


    //풀링
    [SerializeField]
    PoolObjectType objectType;
    List<GameObject> bullets = new List<GameObject>();

    //하트 모양 발사
    float[] speeds = new float[34]; // 모양 만들기 위해 총알 마다 다른 속도 
    float[] dir = new float[34]; //
    float rot = 0f;  // 하트 모양 각도

    //레이저
    [SerializeField]
    private GameObject laser = null;
    [SerializeField]
    private int laserCount = 10; 
    private int laserRot = 0; 
        private void Awake()
    {
        HeartDataInit();
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(aroundShot());
        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(FirecrackerShot());
        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(HeartShot());
        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(aroundOneShot());
        if(Input.GetKeyDown(KeyCode.G))
            StartCoroutine(flowerShot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("보스 ");
        if (isDamaged) return;
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.playerBullet, collision.gameObject);
            enemyCanvasGo.GetComponent<EnemyHPBar>().Damaged();
        }
    }

    public void TargetLaser()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < laserCount; j++)
            {
                Vector2 dir = playerTrans.position - transform.position;

                Ray ray = new Ray(transform.position, dir);

                float MaxDistance = 20f;

                GameObject laser;

                laser = ObjectPool.Instance.GetObject(PoolObjectType.laser);
                laser.transform.rotation = Quaternion.Euler(0, 0, laserRot);
                laser.transform.position += Vector3.down*j*0.2f;
                Debug.DrawRay(transform.position, dir, Color.red, MaxDistance);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, MaxDistance);
                if (hit)
                {
                    enemyCanvasGo.GetComponent<EnemyHPBar>().Damaged();
                }
                
            }
            laserRot += 72;
        }
    }
    IEnumerator aroundShot() //둥글게 발사 
    {
        int shotCount = 93;//개수에 따라 달라지는 총알 사이의 거리 ???

        for (int j = 0; j < 5; j++)
        {
        for (int i = 1; i < shotCount; i++)
        {

            GameObject bullet;
            float radian = Mathf.Rad2Deg * i / shotCount;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
            bullets.Add(bullet);
            bullet.transform.SetParent(transform, true);

            bullet.transform.position = transform.position;// 위치값 초기화
            bullet.transform.rotation = Quaternion.identity; //회전값 초기화  

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            //Vector2 direction = new Vector2(j * 3 + Mathf.Cos(radian), j * 3 + Mathf.Sin(radian));
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

                //yield return new WaitForSeconds(0.05f);
            rigid.AddForce(direction.normalized * 1*speed, ForceMode2D.Impulse);

        }
            yield return new WaitForSeconds(1f);
         }
    }


    IEnumerator aroundOneShot() //둥글게 하나씩 발사 
    {
        int shotCount = 93;//개수에 따라 달라지는 총알 사이의 거리 ???
        int adj = 0; 
        for (int j = 0; j < 5; j++)
        {
            for (int i = 1; i < shotCount; i++)
            {

                GameObject bullet;
                float radian = Mathf.Rad2Deg * i / shotCount;
                bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
                bullets.Add(bullet);
                bullet.transform.SetParent(transform, true);

                bullet.transform.position = transform.position;// 위치값 초기화
                bullet.transform.rotation = Quaternion.identity; //회전값 초기화  

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction; 
                //Vector2 direction = new Vector2(j * 3 + Mathf.Cos(radian), j * 3 + Mathf.Sin(radian));
                if (adj % 2 == 0)
                {
                     direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                }
                else
                {
                     direction = new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
                }
                Debug.Log($"adj :{adj}");
               
                
                //Vector2 dirAdj = new Vector2(j*5, j*5);
                yield return new WaitForSeconds(0.03f);
                rigid.AddForce(direction.normalized * 1 * 100, ForceMode2D.Force);

            }
            adj++;
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator flowerShot() //꽃모양으로 발사
    {
        int shotCount = 93;//개수에 따라 달라지는 총알 사이의 거리 ???
        int adj = 0;
        for (int j = 0; j < 5; j++)
        {
            for (int i = 1; i < shotCount; i++)
            {

                GameObject bullet;
                float radian = Mathf.Rad2Deg * i / shotCount;
                bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
                bullets.Add(bullet);
                bullet.transform.SetParent(transform, true);

                bullet.transform.position = transform.position;// 위치값 초기화
                bullet.transform.rotation = Quaternion.identity; //회전값 초기화  

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction;
                //Vector2 direction = new Vector2(j * 3 + Mathf.Cos(radian), j * 3 + Mathf.Sin(radian));
                if (adj % 2 == 0)
                {
                    direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
                }
                else
                {
                    direction = new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
                }
                adj++;

                //Vector2 dirAdj = new Vector2(j*5, j*5);
                yield return new WaitForSeconds(0.03f);
                rigid.AddForce(direction.normalized * 1 * 100, ForceMode2D.Force);

            }
           
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FirecrackerShot()//플레이어 방향으로 총알 하나 나간후 n초후 터짐 
    {
        GameObject bulletOne;
        bulletOne = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
        bullets.Add(bulletOne);
        bulletOne.transform.SetParent(transform, true);

        bulletOne.transform.position = transform.position;
        bulletOne.transform.rotation = Quaternion.identity;

        Vector2 dir = playerTrans.position - transform.position;
        
       Rigidbody2D rigid = bulletOne.GetComponent<Rigidbody2D>();
      
        rigid.AddForce(dir.normalized * 5, ForceMode2D.Impulse);

        float delay = Random.Range(1f, 2f);

        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 360; i += 20)
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, bulletOne);
            //총알 생성
            GameObject bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type1);
            bullets.Add(bullet);
            //총알 생성 위치를 (0,0) 좌표로 한다.
            bullet.transform.position = bulletOne.transform.position;
            bullet.GetComponent<EnemyBulletTriangle>().speed = 3f; 
            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            bullet.transform.rotation = Quaternion.Euler(0, 0, i);
            //Rigidbody2D rigid2 = bullet.GetComponent<Rigidbody2D>();// 왜 안되지 transform.Translate 로 하면 된다
 
            //rigid2.AddRelativeForce(Vector2.right, ForceMode2D.Impulse);
        }

    }




    IEnumerator  HeartShot() //하트 모양 발사 
    {

        for (int j = 0; j < 5; j++)
        {
            //34개의 게임오브젝트 생성
            for (int i = 0; i < 34; i += 1)
            {
                //오브젝트 생성
                GameObject bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type1);

                bullets.Add(bullet);
                //총알 생성 위치를 (0,0) 좌표로 한다.
                bullet.transform.position = transform.position;

                //정밀한 회전 처리로 모양을 만들어 낸다.
                bullet.transform.rotation = Quaternion.Euler(0, 0, dir[i] + rot);

                //정밀한 속도 처리로 모양을 만들어 낸다.
                bullet.GetComponent<EnemyBulletTriangle>().speed = speeds[i] / 50;
            }
            rot += 72f; 
            yield return new WaitForSeconds(1f);
        }
    }
    void ReturnEnemyBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            switch (bullets[i].tag)
            {
                case "bulet_Type0":
                    ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, bullets[i]);
                    break;
                case "bullet_Type1":
                    ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, bullets[i]);
                    break;
            }
        }
    }

    private void HeartDataInit()
    {
        speeds[0] = 111.00f;
        dir[0] = 90.00f;
        speeds[1] = 133.10f;
        dir[1] = 98.70f;
        speeds[2] = 152.04f;
        dir[2] = 107.37f;
        speeds[3] = 166.88f;
        dir[3] = 116.18f;
        speeds[4] = 176.00f;
        dir[4] = 125.28f;
        speeds[5] = 181.88f;
        dir[5] = 134.29f;
        speeds[6] = 181.50f;
        dir[6] = 143.31f;
        speeds[7] = 175.54f;
        dir[7] = 152.33f;
        speeds[8] = 165.63f;
        dir[8] = 161.38f;
        speeds[9] = 151.50f;
        dir[9] = 170.43f;
        speeds[10] = 136.35f;
        dir[10] = 180.18f;
        speeds[11] = 120.40f;
        dir[11] = 190.90f;
        speeds[12] = 106.45f;
        dir[12] = 203.68f;
        speeds[13] = 98.56f;
        dir[13] = 219.22f;
        speeds[14] = 99.10f;
        dir[14] = 235.97f;
        speeds[15] = 107.97f;
        dir[15] = 251.19f;
        speeds[16] = 124.58f;
        dir[16] = 262.83f;
        speeds[17] = 133.10f;
        dir[17] = 81.30f;
        speeds[18] = 152.04f;
        dir[18] = 72.63f;
        speeds[19] = 166.88f;
        dir[19] = 63.82f;
        speeds[20] = 176.00f;
        dir[20] = 54.72f;
        speeds[21] = 181.88f;
        dir[21] = 45.71f;
        speeds[22] = 181.50f;
        dir[22] = 36.69f;
        speeds[23] = 175.54f;
        dir[23] = 27.67f;
        speeds[24] = 165.63f;
        dir[24] = 18.62f;
        speeds[25] = 151.50f;
        dir[25] = 9.57f;
        speeds[26] = 136.35f;
        dir[26] = 359.82f;
        speeds[27] = 120.40f;
        dir[27] = 349.10f;
        speeds[28] = 106.45f;
        dir[28] = 336.32f;
        speeds[29] = 98.56f;
        dir[29] = 320.78f;
        speeds[30] = 99.10f;
        dir[30] = 304.03f;
        speeds[31] = 107.97f;
        dir[31] = 288.81f;
        speeds[32] = 124.58f;
        dir[32] = 277.17f;
        speeds[33] = 147.85f;
        dir[33] = 270.05f;
    }

   }
    //IEnumerator Allattack()
    //{
    //    float angle = 360 / shotCount;

    //    //불 변수 만들어서 죽으면 true 대신 
    //    while (true)
    //    {
    //        GameObject bullet;

    //        for(int i = 0; i< shotCount; i++)
    //        bullet = Instantiate(Bullet, transform.position,Quaternion.identity);
    //      //  bullet = Instantiate(Bullet, trans);
    //        bullet.GetComponent<Rigidbody2D>().AddForce
    //            (new Vector2(speed * Mathf.Cos(Mathf.PI * i * 2 / shotCount), speed * Mathf.Sin(Mathf.PI * 2 / shotCount)));
    //        //bullet.transform. Rotate(new Vector3(0,0,360 * i / shotCount - 90));
    //        yield return new WaitForSeconds(1f);

    //    }



