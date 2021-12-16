using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    /*
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue;
    */
    public DialogueTemplate Dialogue;
}

[System.Serializable]
public class DialogueTemplate
{
    public string name;
    public Sprite characterSprite;
    [TextArea(2, 3)] public string[] dialogue;
}