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

    public AudioClip clip;
    public void OnClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Enemy"); // 메인 말고 중간 씬 있어야 할 듯
    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void StartTouchSound()
    {
        SoundManager.instance.SFXPlay("Click", clip);
    }

    public void OnClickSetting()
    {
        if (isClickCheck)
        {
            StartTouchSound();
            settingImage.transform.DOScale(new Vector3(0, 0, 0), .25f);
            isClickCheck = false;
        }
        else
        {
            StartTouchSound();
            settingImage.transform.DOScale(new Vector3(1, 1, 1), .25f);
            isClickCheck = true;
        }
    }

    public void OnClickHelp()
    {
        if (isClickCheck)
        {
            StartTouchSound();
            helpImage.transform.DOScale(new Vector3(0, 0, 0), .25f);
            isClickCheck = false;
        }
        else
        {
            StartTouchSound();
            helpImage.transform.DOScale(new Vector3(1, 1, 1), .25f);
            isClickCheck = true;
        }
    }

    public void OnClickDefault()
    {
        StartTouchSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void OnClickExit()
    {
        StartTouchSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
