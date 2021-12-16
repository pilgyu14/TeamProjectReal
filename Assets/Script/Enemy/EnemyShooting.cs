using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : MonoBehaviour
{
    //[SerializeField]
    //private int score = 10;
    [SerializeField]
    private int hp = 2;
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    private GameObject bulletPrefab = null;
    //[SerializeField]
    //private float fireRate = 0.2f;
    [SerializeField]
    private int i = 0;

    private GameObject newBullet = null;
    private Vector2 diff = Vector2.zero;
    private float rotationZ = 0f;

    protected GameManager gameManager = null;
    private Animator animator = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    private bool isDamaged = false;
    public bool isDead = false;

    public AudioClip clip;
    public AudioClip Deadclip;

    private float timer = 0;
    private float waitingTime = 0;

    private StraightDiagonal straightDiagonal; 

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }    
    void Start()
    {
        
        timer = 0;
        waitingTime = 1.5f;
    }

    void Update()
    {
        if (transform.position.y < 6f)
        {
            timer += Time.deltaTime;
            if(timer > waitingTime)
            {
                StartCoroutine(Shooting());
                timer = 0;
            }
        }
        if (isDead) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.CompareTag("playerBullet"))
        {
            Destroy(collision.gameObject);
            if (hp > 1)//���� ���� �� 1�� �ٲ� ��
            {
                if (isDamaged) return;
                isDamaged = true;
                StartCoroutine("Damaged");
                return;
            }
            else
            {
                isDead = true;
                CheckObject();
            }
            //gameManager.AddScore(score);
            StartCoroutine(Dead());
        }
    }

    void CheckObject()
    {
        if(gameObject.GetComponent<StraightDiagonal>())
            gameObject.GetComponent<StraightDiagonal>().IsMove = false;
        else if (gameObject.GetComponent<StraightVertical>())
            gameObject.GetComponent<StraightVertical>().IsMove = false;
        else if (gameObject.GetComponent<StraightCenter>())
            gameObject.GetComponent<StraightCenter>().IsMove = false;
    }
    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        isDamaged = false;
    }

    private IEnumerator Dead()
    {
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        col.enabled = false;
        SoundManager.instance.SFXPlay("Dead", clip);
        // ���� "�������" �׷�
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public IEnumerator Shooting()
    {
        SoundManager.instance.SFXPlay("Shooting", Deadclip);
        diff = GameManager.Instance.player.transform.position - transform.position;
        diff.Normalize();
        for (i = 0; i < 3; i++)
        {
            if (isDead) yield break;
            //newBullet = Instantiate(bulletPrefab, transform);
            newBullet = ObjectPool.Instance.GetObject(PoolObjectType.Bullet);
            newBullet.transform.position = transform.position;
            newBullet.transform.SetParent(null);
            rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            if (i == 1)
            {
                rotationZ = rotationZ - 20;
            }
            else if (i == 2)
            {
                rotationZ = rotationZ + 20;
            }

            newBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);
        }
        yield return new WaitForSeconds(2f);
    }
}
