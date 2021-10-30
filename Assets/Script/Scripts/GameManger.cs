using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    //{
    //    private PoolManager poolManager = null; 
    //    public PoolManager pool { get{ return poolManager; } } 
    //    private void Awake()
    //    {
    //        poolManager = 
    //    } 

    //게임 매니저에 넣기

    public GameObject InstantiateObj(GameObject gameObject)
    {
        return Instantiate(gameObject);
    }

}
