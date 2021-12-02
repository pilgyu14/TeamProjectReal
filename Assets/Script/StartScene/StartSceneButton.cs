using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject settingImage = null;
    [SerializeField] private GameObject helpImage = null;
    private bool isClickCheck = false;

    public void OnClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Enemy"); // 메인 말고 중간 씬 있어야 할 듯
    }

    public void OnClickSetting()
    {
        if (isClickCheck)
        {
            settingImage.transform.DOScale(new Vector3(0, 0, 0), .25f);
            isClickCheck = false;
        }
        else
        {
            settingImage.transform.DOScale(new Vector3(1, 1, 1), .25f);
            isClickCheck = true;
        }
    }

    public void OnClickHelp()
    {
        if (isClickCheck)
        {
            helpImage.transform.DOScale(new Vector3(0, 0, 0), .25f);
            isClickCheck = false;
        }
        else
        {
            helpImage.transform.DOScale(new Vector3(1, 1, 1), .25f);
            isClickCheck = true;
        }
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
