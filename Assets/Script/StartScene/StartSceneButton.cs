using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main"); // 메인 말고 중간 씬 있어야 할 듯
    }

    public void OnClickSetting()
    {

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
