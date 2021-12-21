using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonFunctions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector4 oldColor;
    private GameObject hud;
    private hud hudVariables;
    public int buttonType;
    [SerializeField] GameObject block;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("Hud");
        hudVariables = hud.GetComponent<hud>();
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
        hudVariables.checkType = buttonType;
        hudVariables.isChecked = true;
        hudVariables.block = block;
    }
}
