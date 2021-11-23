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
    bool waypointposition = false;
    void Start()
    {
        
    }

    void Update()
    {
        CheckLimit();
        if(transform.position.y < 6f)
        {
            WayPointMovePath();
            if (waypointposition == true)
                return;
            transform.position = WayPoint[WayPointNum].transform.position;
            waypointposition = true;
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

    private void CheckLimit()
    {
        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}
