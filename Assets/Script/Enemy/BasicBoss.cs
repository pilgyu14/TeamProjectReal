using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicBoss : MonoBehaviour
{
    [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private float speed = 5f;


    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    private Vector2 moveDir;
    bool moveCheck = false;

    private Vector2 basePos = new Vector2(-3.19f, 2.35f);
    [SerializeField]
    private float bulletSpeed = 10f;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    Animator anim;
    //ü�� ���� 
    [SerializeField]
    private GameObject enemyCanvasGo;
    //������
    [SerializeField]
    private Transform parentTrans;
    [SerializeField]
    PowerCode rotationBullet;
    List<GameObject> rotateBullets = new List<GameObject>();
    //Ǯ��
    List<GameObject> bullets = new List<GameObject>();
    //����
    public int patternIndex = -1;
    public int curPatternCount;
    public int[] maxPatternCount;
    int thinkCount = 0;
    List<int> availableNum = new List<int>();
    bool IsPattern = true;
    public int temp = -1; 
    enum BossPatterns
    {
        PowerCode,
        TripleShot,
        flowerShot,
        molu
    }
  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomMaxPatternCount();
        AvailableNumInit();
        think();
    
    }
    void RandomMaxPatternCount() //������ ���� ���� Ƚ�� 
    {
        for (int i = 0; i < maxPatternCount.Length; i++)
        {
            maxPatternCount[i] = Random.Range(1, 4);
        }
        maxPatternCount[(int)BossPatterns.PowerCode] = 1; 
    }
    void Onhit(int dmg)
    {
        hp -= dmg;
        SetColor();
        if (hp <= 0)
        {
            //ObjectPool.Instance.ReturnObject()
            enemyCanvasGo.GetComponent<EnemyHPBar>().Init();
        }
    }
    void AvailableNumInit()
    {
        for (int i = 0; i < maxPatternCount.Length; i++)
        {
            availableNum.Add(i);
        }
    }

    public void think()
    {
        Debug.Log("��ũ");
        Debug.Log("�ƽ� ����ī��Ʈ ����" + maxPatternCount.Length);
        Debug.Log("��ũī��Ʈ" +  thinkCount);
        if(availableNum.Count == 0)
        {
            AvailableNumInit();
            RandomMaxPatternCount();
        }

        curPatternCount = 0;
        thinkCount++; 
        patternIndex = Random.Range(0, availableNum.Count);
        Debug.Log($"�����ε��� {patternIndex} ");
        
        //for (int i = 0; i < availableNum.Count; i++)
        //{
        //    Debug.Log("�����ε��� Ȯ��");
        //    if (patternIndex == temp)
        //    {
        //        Debug.Log("������XX");
        //        IsPattern = false;  
        //        break; 
        //    }
        //}

        //if (patternIndex == 0)
        //    temp = patternIndex;

        //Debug.Log($"��������{IsPattern}");
        //if (IsPattern)
        //{
            Debug.Log("���Ϲ���������");
            switch (availableNum[patternIndex])
            {
                case (int)BossPatterns.PowerCode:
                    {
                        IsPattern = true;
                        PowerCode();
                        //think();
                        break;
                    }
                case (int)BossPatterns.TripleShot:
                    {
                        IsPattern = true;
                        TripleShot();
                        break;
                    }
                case (int)BossPatterns.flowerShot:
                    {
                        IsPattern = true;
                        StartCoroutine(flowerShot());
                        break;
                    }
                case (int)BossPatterns.molu:
                    {
                        IsPattern = true;
                        StartCoroutine(molu());
                        break;
                    }
            }
            
        //}
        //else if(IsPattern == false)
        //{
        //    IsPattern = true;
        //    thinkCount--;
        //    think();
        //}
    }

    void PowerCode()  //������
    {
        curPatternCount++;
        rotateBullets.Clear();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject bullet;
                bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type2);
                bullet.transform.SetParent(parentTrans);
                bullets.Add(bullet);
                rotateBullets.Add(bullet);


                if (j == 0)
                    bullet.transform.position = transform.position + Vector3.right * i;
                else if (j == 1)
                    bullet.transform.position = transform.position + Vector3.up * i;
                else if (j == 2)
                    bullet.transform.position = transform.position + Vector3.left * i;
                else if (j == 3)
                    bullet.transform.position = transform.position + Vector3.down * i;

                if(i == 9)
                      bullet.GetComponent<PowerCodeBullet>().lineNum = i;
            }
        }

        rotationBullet.gameObject.SetActive(true);
        rotationBullet.IsTime = true;
        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            Debug.Log("���� �ٽ�");
            Invoke(nameof(PowerCode), 3f);
        }
    }
    public bool IsBack = false;
    public void MoveToCenter()
    {
        Debug.Log("����������");
        Vector3 targetPos = transform.position;
        for (int i = 0; i < rotateBullets.Count; i++)
        {
            rotateBullets[i].GetComponent<PowerCodeBullet>().IsMove = true;
            // yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("���������ͳ�");
        think();
        availableNum.RemoveAt(patternIndex);
        //IsBack = false;
        //think();

        //ClearRotateBullet();
    }
    public void ClearRotateBullet()
    {
        Debug.Log("Ŭ����Ҹ�");
        for (int i = 0; i < rotateBullets.Count; i++)
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type2, rotateBullets[i]);
        }

    }

    IEnumerator flowerShot() //�ɸ������ �߻�
    {
        Debug.Log("�ɸ�����");
        curPatternCount++;
        Debug.Log(curPatternCount);
            Debug.Log(maxPatternCount[availableNum[patternIndex]]);
        int shotCount = 93;//������ ���� �޶����� �Ѿ� ������ �Ÿ� ???
        int adj = 0;
       // for (int j = 0; j < 5; j++)
        //{
            for (int i = 1; i < shotCount; i++)
            {

                GameObject bullet;
                float radian = Mathf.Rad2Deg * i / shotCount;
                bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
                bullets.Add(bullet);
                bullet.transform.SetParent(transform, true);

                bullet.transform.position = transform.position;// ��ġ�� �ʱ�ȭ
                bullet.transform.rotation = Quaternion.identity; //ȸ���� �ʱ�ȭ  

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
          //  yield return new WaitForSeconds(0.5f);
       // }
        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(flowerShot());
        }
        else
        {
            think();
            availableNum.RemoveAt(patternIndex);
        }
    }
    IEnumerator molu() //���� 
    {
        Debug.Log("�ɸ�����");
        curPatternCount++;
        Debug.Log(curPatternCount);
        Debug.Log(maxPatternCount[availableNum[patternIndex]]);
        int shotCount = 93;//������ ���� �޶����� �Ѿ� ������ �Ÿ� ???
        int adj = 0;

        for (int i = 1; i < shotCount; i++)
        {

            GameObject bullet;
            float radian = Mathf.Deg2Rad * i / shotCount * 360;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
            bullets.Add(bullet);
            //bullet.transform.SetParent(transform, true);
            bullet.transform.SetParent(null);
            bullet.transform.position = transform.position;// ��ġ�� �ʱ�ȭ
            bullet.transform.rotation = Quaternion.identity; //ȸ���� �ʱ�ȭ  

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
            rigid.AddForce(direction.normalized * 1 * 200, ForceMode2D.Force);

        }

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            Debug.Log("dd");
            yield return new WaitForSeconds(1.5f);
                StartCoroutine(molu());
        }
        else
        {
            Debug.Log("�����ũ");
            think();
            availableNum.RemoveAt(patternIndex);
        }
    }



    void TripleShot()
    {
        curPatternCount++;
        GameObject bulletL = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);
        GameObject bulletR = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);
        GameObject bulletC = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);

        bulletL.transform.position = transform.position + Vector3.right * 0.5f;
        bulletR.transform.position = transform.position + Vector3.left * 0.5f;
        bulletC.transform.position = transform.position;

        Rigidbody2D rigid_L = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid_R = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigid_C = bulletC.GetComponent<Rigidbody2D>();

        Vector2 dir = player.transform.position - transform.position;

        rigid_L.AddForce((dir + new Vector2(-1, 0)).normalized * bulletSpeed, ForceMode2D.Impulse);
        rigid_R.AddForce((dir + new Vector2(1, 0)).normalized * bulletSpeed, ForceMode2D.Impulse);
        rigid_C.AddForce(dir.normalized * bulletSpeed, ForceMode2D.Impulse);

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            Invoke(nameof(TripleShot), 0.5f);
        }
        else
        {
            Debug.Log("Ʈ���ü�����");
            think();
            availableNum.RemoveAt(patternIndex);
        }

    }
    private void Move()
    {
        if (moveCheck == false)
        {
            moveDir = Vector2.right;
            moveCheck = true;
        }
        transform.Translate(moveDir * speed * Time.deltaTime);
        if (transform.position.x >= GameManager.Instance.maxPos.x)
        {
            Debug.Log("����");
            moveDir = Vector2.left;
        }

        if (transform.position.x <= GameManager.Instance.minPos.x)
        {
            Debug.Log("������");
            moveDir = Vector2.right;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("�浹��");
        //Debug.Log(collision.name);
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

//void think2()
//{
//    Debug.Log("1");
//    int[] exceptPatterns = new int[5];
//    patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
//    Debug.Log("�����ε���" + patternIndex);
//    curPatternCount = 0;
//    switch (patternIndex)
//    {
//        case 0:
//            {
//                PowerCode();
//                break;
//            }
//        case 1:
//            {
//                TripleShot();
//                break;
//            }
//        case 2:
//            {
//                StartCoroutine(flowerShot());
//                break;
//            }
//        case 3:
//            {
//                StartCoroutine(molu());
//                break;
//            }
//    }
//}