using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMsaileTest : MonoBehaviour
{

    float rotateSpeed = 500f;
    float moveSpeed = 3f; 
    private Transform targetTrs;
    private float distance;
    Rigidbody2D rigid;
    float axis = 0f;

    bool isTrack = true;
    float endDistance = 6f;

    public Vector3 targetPos;
    public bool IsTargetting = false;
    float time = 0;

    private void OnEnable()
    {
        IsTargetting = false; 
    }
    void Start()
    {
        targetTrs = FindObjectOfType<Player>().transform;
        rigid = GetComponent<Rigidbody2D>(); 
     }

                                                                            
    void Update()
    {
        check(); 
        if (IsTargetting)
        {
             target();
             Move();
        }
        else
            MovePoint();

    }
    public void MovePoint()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
        transform.Translate(targetPos.normalized * Time.deltaTime * moveSpeed);
        time += Time.deltaTime;
        if (time >= 1f)
        {
            IsTargetting = true;
            moveSpeed = Random.Range(3f, 8f);
        }
     }
    void target()
    {
        if (isTrack == false) return; 
        Vector3 myPos = transform.position;
        Vector3 targetPos = targetTrs.position;

        //targetPos.z = myPos.z;

        Vector3 vectorToTarget = myPos - targetPos;

        distance = (targetPos - myPos).sqrMagnitude;
        //Debug.Log(distance);
        if (distance < endDistance) { Debug.Log("ÃßÀû ³¡"); isTrack = false;  } 
        Vector3 quaternionToTarget = vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
    }
    private void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
    void check()
    {
        if (transform.position.x > GameManager.Instance.maxPos.x + 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type3, gameObject);
        if (transform.position.x < GameManager.Instance.minPos.x - 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type3, gameObject);
        if (transform.position.y > GameManager.Instance.maxPos.y + 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type3, gameObject);
        if (transform.position.y < GameManager.Instance.minPos.y - 3)
            ObjectPool.Instance.ReturnObject(PoolObjectType.bullet_Type3, gameObject);

    }
}
