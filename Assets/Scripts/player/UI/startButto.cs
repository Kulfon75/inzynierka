using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class startButto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector4 oldColor;
    [SerializeField] private gridMenager grMen;
    [SerializeField] private InputField InputX, InputY;
    [SerializeField] private Canvas inputField;
    // Start is called before the first frame update
    void Start()
    {
        
        oldColor = GetComponent<Image>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Vector4(oldColor.x, oldColor.y, oldColor.z, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = oldColor;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        grMen.createGrid(int.Parse(InputX.text), int.Parse(InputY.text), 20);
        grMen.generated = true;
        inputField.enabled = false;
    }
}
