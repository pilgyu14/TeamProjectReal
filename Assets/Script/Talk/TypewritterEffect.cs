using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewritterEffect : MonoBehaviour
{
    [SerializeField] private float typewritterSpeed = 50f;

    public Coroutine Run(string textTOType, Text textLabel)
    {
        return StartCoroutine(routine: TypeText(textTOType, textLabel));
    }

    private IEnumerator TypeText(string textTOType, Text textLabel)
    {
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while(charIndex < textTOType.Length)
        {
            t += Time.deltaTime * typewritterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textTOType.Length);

            textLabel.text = textTOType.Substring(startIndex: 0, length: charIndex);

            yield return null;  
        }
        textLabel.text = textTOType;
    }
}
