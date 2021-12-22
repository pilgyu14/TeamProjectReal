using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    #region 대화 목록
    public Conversation conversation;
    #endregion

    public GameObject talkerLeft;
    public GameObject talkerRight;

    public TalkUI talkerUILeft;
    public TalkUI talkerUIRight;

    private int activeLineIndex = 0;

    private void Start()
    {
        talkerUILeft = talkerLeft.GetComponent<TalkUI>();
        talkerUIRight = talkerRight.GetComponent<TalkUI>();

        talkerUILeft.Talker = conversation.talkerLeft;
        talkerUIRight.Talker = conversation.talkerRight;
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
        activeTalkerUI.Dialog = text;
        activeTalkerUI.Show();
        inactiveTalkerUI.Hide();
    }
}