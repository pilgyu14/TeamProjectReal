using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : EnemyShooting
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

    private Animator animator = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    private bool isDamaged = false;
    private bool isDead = false;

    public AudioClip Laserclip;

    private float timer = 0;
    private float waitingTime = 0;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = 0;
        waitingTime = 1.5f;
    }

    private void Update()
    {
        if (transform.position.y < 6f)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                StartCoroutine(Shooting());
                timer = 0;
            }
        }
        if (isDead) return;
    }
    public override IEnumerator Shooting()
    {
        //if (isDead == false) yield break;
        diff = GameManager.Instance.player.transform.position - transform.position;
        diff.Normalize();
        for (i = 0; i < 3; i++)
        {
            //newBullet = Instantiate(bulletPrefab, transform);
            newBullet = ObjectPool.Instance.GetObject(PoolObjectType.Bullet);
            newBullet.transform.position = transform.position;
            newBullet.transform.SetParent(null);
            rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        }
        yield return new WaitForSeconds(2f);
    }
    protected override IEnumerator Dead()
    {
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        col.enabled = false;
        SoundManager.instance.SFXPlay("Dead", clip);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

