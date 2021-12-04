using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scenemanager : MonoBehaviour
{
    [SerializeField]
    private GameObject Emilia;
    [SerializeField]
    private GameObject ProjectSText;
    [SerializeField]
    private GameObject RestartButton;
    [SerializeField]
    private GameObject QuitButton;

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        Emilia.transform.DOMove(new Vector3(-4.6f, 0f, 0f), 2f);
        seq.Insert(0.75f, ProjectSText.transform.DOMove(new Vector3(4f, 3f, 0f), 2f));
        seq.Append(RestartButton.transform.DOMove(new Vector3(1.35f, 0.5f, 0f), 2f));
        seq.Join(QuitButton.transform.DOMove(new Vector3(6f, 0.5f, 0f), 2f));
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
}
