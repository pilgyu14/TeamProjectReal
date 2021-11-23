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
    float endDistance = 0.3f; 
    void Start()
    {
        targetTrs = FindObjectOfType<player>().transform;
        rigid = GetComponent<Rigidbody2D>(); 
     }

                                                                            
    void Update()
    {
        target();
        Move(); 
    }
    void rota()
    {
        Vector2 direction =GameManager.Instance.curDir;
        rigid.AddForce(direction,ForceMode2D.Impulse);
        //transform.rotation = Quaternion.Euler(0, 0, GameManager.Instance.curDir.);
    }
    void target()
    {
        if (isTrack == false) return; 
        Vector3 myPos = transform.position;
        Vector3 targetPos = targetTrs.position;

        //targetPos.z = myPos.z;

        Vector3 vectorToTarget = myPos - targetPos;

        distance = (targetPos - myPos).sqrMagnitude;
        Debug.Log(distance);
        if (distance < endDistance) { Debug.Log("ÃßÀû ³¡"); isTrack = false;  } 
        Vector3 quaternionToTarget = vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
    }
    private void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}
