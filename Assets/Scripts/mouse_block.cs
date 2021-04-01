using UnityEngine;

public class mouse_block : MonoBehaviour
{
    private Color block_color;
    private Color start_block_color;
    void Start()
    {
        start_block_color = GetComponent<SpriteRenderer>().color;
        block_color = GetComponent<SpriteRenderer>().color;
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = block_color;
    }
    public void IsActive(bool is_active)
    {
        if(!is_active)
        {
            block_color = start_block_color;
            GetComponent<SpriteRenderer>().color = block_color;
        }
        else
        {
            block_color = Color.blue;
        }
    }
}
