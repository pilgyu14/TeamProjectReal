using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    public GameObject Player = null;

    public Player player
    {
        get;
        private set; 
    }

    public GameObject boss; 
    private Vector2 curMinPos = new Vector2(-8.8f, -4.9f);
    private Vector2 curMaxPos = new Vector2(3.15f, 4.9f);
    private Vector2 direction;
    public Vector2 curDir
    {
        get { return direction; }
        private set { }
    }
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        //direction = player.transform.position - boss.transform.position;
    }
}
