using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 3)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName ="Conversation")]
public class Conversation : ScriptableObject
{
    public Character talkerLeft;
    public Character talkerRight;
    public Line[] lines;
}
