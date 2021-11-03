using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerSkill : MonoBehaviour
{
    private CircleCollider2D col;

    //스킬 쿨타임
    [SerializeField]
    private Image coolTime;
    private float timeCount = 0f;
    private float maxTime = 100f; 
    private float minusCount = 3f;
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
            IsCoolTime = true;
            timeCount = 100f; 
        }
        DoCoolTime();
        timeCount -= minusCount * Time.deltaTime;
    }
    void Explossion()
    {
        col.enabled = true;
        transform.DOScale(new Vector3(70, 1, 1), 1f).OnComplete(() => col.enabled = false);
        transform.localScale = new Vector3(1, 1, 1);

    }

    void DoCoolTime()
    {
        if (coolTime.fillAmount == 0)
            IsCoolTime = false; 
        coolTime.fillAmount = timeCount / 100f; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("스킬 충돌");
        if (collision.tag == "player" || collision.tag == "playerBullet") return;
        if(collision.tag == "bullet_Type0")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type0, collision.gameObject);
        else if(collision.tag == "bullet_Type1")
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type1, collision.gameObject);
    }
}
