using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class place_room : MonoBehaviour, IPointerDownHandler
{
    public GameObject room;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        position = GetComponentInParent<hud>().block.transform.position;
        Destroy(GetComponentInParent<hud>().block);
        Instantiate(room, position, Quaternion.identity);
    }
}
