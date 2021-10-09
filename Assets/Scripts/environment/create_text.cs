using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_text : MonoBehaviour
{  
    public TextMesh CreateWordText(Transform parent, string text, Vector3 position, int fontSize, Color color, TextAnchor anchor)
    {
        GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
        gameObject.transform.SetParent(parent.transform);
        Transform transform = gameObject.transform;
        transform.localPosition = position;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = anchor;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        return textMesh;
    }
}
