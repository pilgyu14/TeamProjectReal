using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject stopImage;
    [SerializeField] private Text timeCheckText1;
    [SerializeField] private Text timeCheckText2;
    [SerializeField] private Text timeCheckText3;
    [SerializeField] private GameObject reallyExitImage;
    [SerializeField] private GameObject helpImage;

    private bool isClickingCheck = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopButton();
        }
    }

    public void StopButton()
    {
        if (isClickingCheck)
        {
            stopImage.SetActive(false);
            isClickingCheck = false;
            /*
            Sequence seq = DOTween.Sequence();
            seq.Append(timeCheckText1.DOFade(1f, 0f));
            seq.Append(timeCheckText1.DOFade(0f, 1f));
            seq.Append(timeCheckText2.DOFade(1f, 0f));
            seq.Append(timeCheckText2.DOFade(0f, 1f));
            seq.Append(timeCheckText3.DOFade(1f, 0f));
            seq.Append(timeCheckText3.DOFade(0f, 1f));
            Debug.Log("asdf");
            Invoke("TimeScaleToOne", 3);
            */
            TimeScaleToOne();
        }
        else
        {
            Time.timeScale = 0f;
            stopImage.SetActive(true);
            isClickingCheck = true;
        }
    }
    public void TimeScaleToOne()
    {
        Time.timeScale = 1f;
    }

    public void ExitGameButton()
    {
        reallyExitImage.SetActive(true);
    }
    public void ReallyExitGameOButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ReallyExitGameXButton()
    {
        reallyExitImage.SetActive(false);
    }

    public void HelpShow()
    {
        //helpImage.transform.DOScale(new Vector3(1, 1, 1), .25f); //타임스케일떄문에 안움직임
        helpImage.SetActive(true);
    }

    public void HelpClose()
    {
        //helpImage.transform.DOScale(new Vector3(0, 0, 0), .25f);
        helpImage.SetActive(false);
    }
}
