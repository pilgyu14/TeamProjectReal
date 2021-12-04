using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeSlow : MonoBehaviour
{
    Player play;

    private CircleCollider2D col;
    private Sprite sprite;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        sprite = GetComponent<Sprite>();
        play = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TimeSlowSkill();
        }
    }
    void TimeSlowSkill()
    {
        Debug.Log("Time");
        col.enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(6, 6, 6), 1.5f));
        seq.AppendInterval(10f);
        seq.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f));
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("타이무스토푸");
        if (collision.CompareTag("player"))
            play.isTimeSlow = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("토기와토마레");
        if (collision.CompareTag("player"))
        {
            play.isTimeSlow = false;
            play.curSpeed = 5f;
        }
    }
}
