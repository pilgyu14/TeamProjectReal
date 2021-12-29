using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalkUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
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
    public bool TalkerIs(Character character)
    {
        return talker == character;
    }
    public void Show()
    {
        canvasGroup.DOFade(1, .5f);
    }
    public void Hide()
    {
        canvasGroup.DOFade(0, .5f);
    }
    public void TextSmooth()
    {
        
        //dialog.DOText(dialog.text, 2);
    }
}