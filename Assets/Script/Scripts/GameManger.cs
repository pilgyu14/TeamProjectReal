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

    //���� �Ŵ����� �ֱ�

    public GameObject InstantiateObj(GameObject gameObject)
    {
        return Instantiate(gameObject);
    }

}
