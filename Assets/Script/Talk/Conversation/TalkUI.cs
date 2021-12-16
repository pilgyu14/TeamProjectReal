using UnityEngine;
using UnityEngine.UI;

public class TalkUI : MonoBehaviour
{
    public Image characterImage;
    public Text nameText;
    public Text dialog;

    private Character talker;
    public Character Talker
    {
        get { return talker; }
        set
        {
            talker = value;
            characterImage.sprite = talker.characterImage;
            nameText.text = talker.name;
        }
    }
    public string Dialog
    {
        set { dialog.text = value; }
    }
    public bool HasTalker()
    {
        return talker != null;
    }
    public bool TalkerIs(Character  character)
    {
        return talker == character;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}