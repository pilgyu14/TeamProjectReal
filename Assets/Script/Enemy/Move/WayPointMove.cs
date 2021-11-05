using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField]
    Transform[] WayPoint; //위치 배열
    [SerializeField]
    float speed = 3f; 
    int WayPointNum = 0; 
    void Start()
    {
        transform.position = WayPoint[WayPointNum].transform.position;
    }

    void Update()
    {
        if(transform.position.y < 6f)
        {
            WayPointMovePath();
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        
    }

    private void WayPointMovePath()
    {
        transform.position = Vector2.MoveTowards
            (transform.position, WayPoint[WayPointNum].transform.position, speed * Time.deltaTime);

        if(transform.position == WayPoint[WayPointNum].transform.position)
        {
            WayPointNum++;
        }

        if(WayPointNum == WayPoint.Length)
        {
            WayPointNum = 0;
        }
    }
}
