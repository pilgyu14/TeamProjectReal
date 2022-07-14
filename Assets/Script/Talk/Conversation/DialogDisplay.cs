using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation = null;

    public GameObject talkerLeft;
    public GameObject talkerRight;

    public TalkUI talkerUILeft;
    public TalkUI talkerUIRight;

    public GameObject endSceneImage;

    private int activeLineIndex = 0;

    private void Start()
    {
        talkerUILeft = talkerLeft.GetComponent<TalkUI>();
        talkerUIRight = talkerRight.GetComponent<TalkUI>();

        talkerUILeft.Talker = conversation.talkerLeft;
        talkerUIRight.Talker = conversation.talkerRight;

        AdvanceConversation();
    }
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AdvanceConversation();
        }
    }
    void AdvanceConversation()
    {
        if (activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
        }
        else
        {
            talkerUILeft.Hide();
            talkerUIRight.Hide();
            activeLineIndex = 0;
            endSceneImage.transform.DOMove(new Vector3(0, 0), 1)
                .OnComplete(() =>SceneManager.LoadScene("Boss1"));
        }
    }
    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (talkerUILeft.TalkerIs(character))
        {
            SetDialog(talkerUILeft, talkerUIRight, line.text);
        }
        else
        {
            SetDialog(talkerUIRight, talkerUILeft, line.text);
        }
    }
    void SetDialog(TalkUI activeTalkerUI, TalkUI inactiveTalkerUI, string text)
    {
        inactiveTalkerUI.Hide();
        activeTalkerUI.Dialog = text;
        activeTalkerUI.Show();
        //activeTalkerUI.TextSmooth();
        // DOText전에 text비우기

    }
}