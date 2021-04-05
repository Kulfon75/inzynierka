using UnityEngine;

public class create_grid : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    // Start is called before the first frame update
    void Start()
    {
        createGrid(10, 10, 20);
    }
    public void createGrid(int x, int y, int width)
    {
        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {  
                Transform foo = Instantiate(block, new Vector3(i * width, j * width, 1), Quaternion.identity);
                foo.GetComponent<mouse_block>().placeX = i + 1;
                foo.GetComponent<mouse_block>().placeY = j + 1;
            }
        }
        hud.GetComponentInChildren<place_room>().setTakenFalse(x,y);
    }
}
