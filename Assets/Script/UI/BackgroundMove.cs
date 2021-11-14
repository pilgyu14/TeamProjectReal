using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprite;

    float viewHeight;

    public void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        Vector2 curPos = transform.position;
        Vector2 nextPos = Vector2.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (sprite[endIndex].position.y < viewHeight*(-1))
        {
            Vector3 backSpritePos = sprite[startIndex].localPosition + Vector3.down;
            Vector3 fromspritePos = sprite[endIndex].localPosition + Vector3.up;
            sprite[endIndex].transform.localPosition = backSpritePos + Vector3.up * 10;

            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprite.Length - 1 : startIndexSave - 1;
        }
    }
}
