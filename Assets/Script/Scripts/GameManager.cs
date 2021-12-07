using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool isStageClear = false;

    public Player player;

    //public Player player
    //{
    //    get;
    //    private set; 
    //}

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<Player>();
    }
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DontDestroyOnLoad(gameObject);
        player = FindObjectOfType<Player>();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void FixedUpdate()
    {
        //direction = player.transform.position - boss.transform.position;
    }
}
