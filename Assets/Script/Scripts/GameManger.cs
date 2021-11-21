using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Vector2 curMinPos = new Vector2(-8.8f, -4.9f);
    private Vector2 curMaxPos = new Vector2(3.15f, 4.9f);
    public Vector2 minPos
    {
        get
        {
            return curMinPos;
        }
    }
    public Vector2 maxPos
    {
        get
        {
            return curMaxPos;
        }
    }
}
