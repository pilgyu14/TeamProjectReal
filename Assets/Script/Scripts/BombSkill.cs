using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class BombSkill : MonoBehaviour
{
    private CircleCollider2D col;

    //스킬 쿨타임
    [SerializeField]
    private Image coolTime;
    [SerializeField] private Text coolTimeText; 
    private float timeCount = 0f;
    private float maxTime = 45f; 
    private float minusCount = 1f;
    private bool IsCoolTime = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        //coolTime = GetComponent<Image>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (IsCoolTime == true) return;
            Explossion();
            timeCount = maxTime;
            IsCoolTime = true; 
            Debug.Log("2");
        }
        if (IsCoolTime)
        {
            coolTimeText.gameObject.SetActive(true);
            DoCoolTime();
            timeCount -= minusCount * Time.deltaTime;
        }
    }
    void UpdateUI()
    {
        coolTimeText.text = string.Format("{0:F0}", timeCount);
    }
    void Explossion()
    {
        Debug.Log("1");
        col.enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(50, 50, 50), 1.5f));
        seq.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.1f));
        col.enabled = true;
    }

    void DoCoolTime()
    {
        coolTime.fillAmount = timeCount / maxTime;
        UpdateUI();
        if (coolTime.fillAmount == 0)
        {
            IsCoolTime = false;
            coolTimeText.gameObject.SetActive(false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("스킬 충돌");
        if (collision.tag == "bullet_Type0")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, collision.gameObject);
        else if (collision.tag == "bullet_Type1")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, collision.gameObject);
        else if (collision.tag == "bullet_Type2")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type2, collision.gameObject);
        else if (collision.tag == "bullet_Type3")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, collision.gameObject);
        else if (collision.tag == "Bullet")
            ObjectPool.Instance.ReturnObject(PoolObjectType.Bullet, collision.gameObject);
    }
}
