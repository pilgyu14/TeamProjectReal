using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss2 : MonoBehaviour
{   [SerializeField]
    private float hp = 100f;
    [SerializeField]
    private float speed = 5f;
    Vector3 basicPos = new Vector3(-3.19f, 2.35f);

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
    //Ǯ��
    List<GameObject> bullets = new List<GameObject>();
    //����
    public int patternIndex = -1;
    public int movePatternIndex; 
    public int curPatternCount;
    public int curMovePatternCount;
    [Header("���� Ƚ�� ����")]
    public int[] maxPatternCount;
    public int[] maxMovePatternCount; 
    int thinkCount = 0;
    List<int> availableNum = new List<int>();
    List<int> availableMoveNum = new List<int>(); 

    //��Ʈ ��� �߻�
    float[] speeds = new float[34]; // ��� ����� ���� �Ѿ� ���� �ٸ� �ӵ� 
    float[] dir = new float[34]; //
    float rot = 0f;  // ��Ʈ ��� ����
                     //�� ������ ����
    List<GameObject> circleBullets = new List<GameObject>();
    // ���ư��鼭 �߻�
    private int dirAdj = 0;
    bool IsMoving = true;
    enum BossPatterns
    {
        HeartShot,
        Missile,
        aroundOneShot,
        CircleToTarget,
        SmallCircleTarget
    }
    enum MovePatterns
    {
        LeftRightMove,
        Teleportation
    }
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        HeartDataInit();
        availableNumInit();
        RandomMaxPatternCount();
        think();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))    
            Missile(); 
    }
    void availableNumInit() 
    {
        for (int i = 0; i < maxPatternCount.Length; i++)
        {
            availableNum.Add(i);
            Debug.Log(availableNum[i]);
        }
    }
    void availableMoveNumInit()
    {
        for (int i = 0; i < maxMovePatternCount.Length; i++)
        {
            availableMoveNum.Add(i);
            Debug.Log(availableNum[i]);
        }
    }
    void RandomMaxPatternCount() //������ ���� ���� Ƚ�� 
    {
        for (int i = 0; i < maxPatternCount.Length; i++)
        {
            maxPatternCount[i] = Random.Range(1, 4);
            if(maxPatternCount[i] == (int)BossPatterns.aroundOneShot)
                maxPatternCount[i] = Random.Range(1, 3);
        }
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
    public void think()
    {
        Debug.Log("��ũ");
   
        if (availableNum.Count == 0)
        {
            Debug.Log("����Ʈ�ʱ�ȭ");
            thinkCount = 0;
            availableNumInit();
            RandomMaxPatternCount();
        }

        if(availableMoveNum.Count == 0)
        {
            availableMoveNumInit(); 
        }

        curPatternCount = 0;
        thinkCount++; 
        patternIndex = Random.Range(0, availableNum.Count);
        
        Debug.Log($"�����ε��� {patternIndex} ");
        Debug.Log("����" + availableNum[patternIndex]);

        Debug.Log("���Ϲ���������");
    
            switch (availableNum[patternIndex])
            {
                case (int) BossPatterns.HeartShot:
                    {
                        HeartShot(); 
                        break;
                    }
                case (int)BossPatterns.Missile:
                    {
                        Missile();
                        break;
                    }
                case (int)BossPatterns.aroundOneShot:
                    {
                        StartCoroutine(aroundOneShot());
                        break;
                    }
                case (int)BossPatterns.CircleToTarget:
                    {
                        CircleToTarget(); 
                        break;
                    }
            }

        if (IsMoving)
        {
            if (thinkCount % 2 == 0)
            {
                curMovePatternCount = 0;
                Debug.Log("���� ������");
                movePatternIndex = Random.Range(0, availableMoveNum.Count);
                Debug.Log("���������ε���"+ movePatternIndex);
                switch (availableMoveNum[movePatternIndex])
                {
                    case (int)MovePatterns.LeftRightMove:
                        {
                            Debug.Log("�¿�");
                            IsMoving = false;
                            LeftRightMove();
                            break;
                        }
                    case (int)MovePatterns.Teleportation:
                        {
                            Debug.Log("����");
                            IsMoving = false;
                            Teleportation();
                            break;
                        }
                }
               
            }

        }
    }
    IEnumerator aroundOneShot()
    {
        Debug.Log("������Ŭ");
        float delay = 1f;
        curPatternCount++;
        int shotCount = 93;//������ ���� �޶����� �Ѿ� ������ �Ÿ� ???
        dirAdj++;
        for (int i = 1; i < shotCount; i++)
        {
            GameObject bullet;
            float radian = Mathf.Rad2Deg * i / shotCount;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);//(Bullet, trans);
            bullets.Add(bullet);
            //bullet.transform.SetParent(transform, true);
            bullet.transform.SetParent(null);
            bullet.transform.position = transform.position;// ��ġ�� �ʱ�ȭ
            bullet.transform.rotation = Quaternion.identity; //ȸ���� �ʱ�ȭ  

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction;
            //Vector2 direction = new Vector2(j * 3 + Mathf.Cos(radian), j * 3 + Mathf.Sin(radian));
            if (dirAdj % 2 == 0)
            {
                direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            }
            else
            {
                direction = new Vector2(Mathf.Sin(radian), Mathf.Cos(radian));
            }
            yield return new WaitForSeconds(0.03f);
            rigid.AddForce(direction.normalized * 1 * 150, ForceMode2D.Force);
        }
        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(aroundOneShot());
        }
        else
        {
            Invoke(nameof(think), 1f);
            availableNum.RemoveAt(patternIndex);
        }

    }

    void HeartShot()
    {
        Debug.Log("��Ʈ��");
        float delay = 1f; 
        curPatternCount++;

        int angelChange = 360 / maxPatternCount[(int)BossPatterns.HeartShot];

            //34���� ���ӿ�����Ʈ ����
            for (int i = 0; i < 34; i += 1)
            {
                //������Ʈ ����
                GameObject bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type1);

                bullets.Add(bullet);
            bullet.transform.SetParent(null);
                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                bullet.transform.position = transform.position;

                //������ ȸ�� ó���� ����� ����� ����.
                bullet.transform.rotation = Quaternion.Euler(0, 0, dir[i] + rot);
                
                //������ �ӵ� ó���� ����� ����� ����.
                bullet.GetComponent<EnemyBulletTriangle>().speed = speeds[i] / 50;
            }
            rot += angelChange;
        

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
            Invoke(nameof(HeartShot), delay);
        else
        {
            Invoke(nameof(think), 1f);
            availableNum.RemoveAt(patternIndex);
            rot = 0f; 
        }
        }

    void Missile()
    {
        Debug.Log("��������");
        float delay = 1f;
        curPatternCount++;

        GameObject bullet;
        int count = Random.Range(3, 6);
        for (int i = 0; i < count; i++)
        {
            float radian = 180 * i / count * Mathf.Deg2Rad;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type3);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<GuidedMsaileTest>().targetPos = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0).normalized;
        }

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            Invoke(nameof(Missile),delay);
        }
        else
        {
            Invoke(nameof(think), 1f);
            availableNum.RemoveAt(patternIndex);
        }
        }

    void CircleToTarget()
    {
        Debug.Log("��Ŭ��Ÿ��");
        float delay = 0.5f;
        curPatternCount++;

        float count = 16;
        float radian;
        List<GameObject> bulletsGo = new List<GameObject>();
        float speed = Random.Range(3, 5);
        Debug.Log("ù����" + speed);
        for (int i = 0; i < count; i++)
        {
            radian = (i / count) * 360;
            GameObject bullet;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type1);
            bullet.GetComponent<EnemyBulletTriangle>().speed = speed; 
            bullets.Add(bullet);

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, radian);
            bulletsGo.Add(bullet);
        }
        StartCoroutine(BulletToTarget(bulletsGo));

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
            Invoke(nameof(CircleToTarget), delay);
        else
        { 
            Invoke(nameof(think), 1f);
        availableNum.RemoveAt(patternIndex);
         }
    }
    IEnumerator BulletToTarget(List<GameObject> bulletsGo)
    {
        float delay = 0.5f;
        yield return new WaitForSeconds(delay);
        Debug.Log("����");
        Vector3 direction;
        float speed = Random.Range(6, 12);
        Debug.Log("�ι�������" + speed);
        float angle;
        for (int i = 0; i < bulletsGo.Count; i++)
        {
            direction = player.transform.position - bulletsGo[i].transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletsGo[i].transform.rotation = Quaternion.Euler(0, 0, angle);
            bulletsGo[i].GetComponent<EnemyBulletTriangle>().speed = speed; 
        }
    }

    IEnumerator SmallCircle()
    {
        Debug.Log("������Ŭ");
        float delay = 1f;
        curPatternCount++;

        float count = 16f;
        float shotDelay = 0.1f;
        float angle;
        for (int i = 0; i < count; i++)
        {
            GameObject bullet;
            angle = i / count * 360;
            bullet = ObjectPool.Instance.GetObject(PoolObjectType.bullet_Type0);
            circleBullets.Add(bullet);
            bullets.Add(bullet);

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            //bullet.transform.Translate(Vector2.right);
            yield return new WaitForSeconds(shotDelay);
        }
        StartCoroutine(SmallCircleShot());

        if (curPatternCount < maxPatternCount[availableNum[patternIndex]])
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(SmallCircle());
        }
        else
        {
            Invoke(nameof(think), 1f);
            availableNum.RemoveAt(patternIndex);
        }

        }
    IEnumerator SmallCircleShot()
    {
        Vector2 direction;
        Rigidbody2D rigid;
        float speed = Random.Range(50,100); ;
        float delay = 0.2f;
        for (int i = 0; i < circleBullets.Count; i++)
        {
            speed += 0.5f;
            rigid = circleBullets[i].GetComponent<Rigidbody2D>();
            direction = player.transform.position - circleBullets[i].transform.position;
            rigid.AddForce(direction * speed);

            yield return new WaitForSeconds(delay);
        }
    }

    Vector2 randomX;
    void Teleportation()
    {
        float delay = 0.5f; 
        curMovePatternCount++;
        Debug.Log("���� ����" + curMovePatternCount);
        randomX = new Vector2(Random.Range(GameManager.Instance.minPos.x, GameManager.Instance.maxPos.x), transform.position.y);
        //�ִϸ��̼� ���� 
        transform.position = randomX;

        if (curMovePatternCount < maxMovePatternCount[movePatternIndex])
        {
            Invoke(nameof(Teleportation), delay);
        }

        if (curMovePatternCount== maxMovePatternCount[movePatternIndex])
        {
                transform.position = basicPos;
            IsMoving = true;
            availableMoveNum.RemoveAt(movePatternIndex);
        }
    }
    private void LeftRightMove()
    {
       
        float delay = 0.5f;
        int loopsCount = 6; 
        curMovePatternCount++;
        Debug.Log("�¿� ����" + curMovePatternCount);

        transform.DOMoveX(GameManager.Instance.minPos.x, 2f)
            .OnComplete(() =>
        transform.DOMoveX(GameManager.Instance.maxPos.x, 4f).SetLoops(loopsCount, LoopType.Yoyo)
        .OnComplete(() =>
        transform.DOMoveX(basicPos.x, 1f)
        .OnComplete(() =>
        IsMoving = true)));


        //if (curMovePatternCount == maxMovePatternCount[movePatternIndex])
        //{
            availableMoveNum.RemoveAt(movePatternIndex);
        //}
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


