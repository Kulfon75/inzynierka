using UnityEngine;
using UnityEngine.EventSystems;

public class place_room : MonoBehaviour, IPointerDownHandler
{
    public GameObject room;
    private Vector3 position_on_board;
    public Grid position_in_array;
    private int pos_array_x;
    private int pos_array_y;
    public bool[,] isTaken;
    public GameObject[,] floorOb;
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
        if (GetComponentInParent<hud>().block != null)
        {
            pos_array_x = GetComponentInParent<hud>().block.GetComponent<mouse_block>().placeX;
            pos_array_y = GetComponentInParent<hud>().block.GetComponent<mouse_block>().placeY;
            isTaken[pos_array_x, pos_array_y] = true;
            position_on_board = GetComponentInParent<hud>().block.transform.position;
            Destroy(GetComponentInParent<hud>().block);
            GetComponentInParent<hud>().block = null;
            GameObject foo = Instantiate(room, position_on_board, Quaternion.identity);
            foo.GetComponent<mouse_block>().placeX = pos_array_x;
            foo.GetComponent<mouse_block>().placeY = pos_array_y;
            foo.GetComponent<mouse_block>().colide = true;
            SetArrayValueFloor(pos_array_x, pos_array_y, foo.gameObject);
        }
    }
    public void setTakenFalse(int x, int y)
    {
        floorOb = new GameObject[x + 2, y + 2];
        isTaken = new bool[x + 2, y + 2];
        for (int i = 0; i <= x + 1; i++)
        {
            for (int j = 0; j <= y + 1; j++)
            {
                isTaken[x, y] = false;
            }
        }
    }
    public void SetArrayValueFloor(int x, int y, GameObject floor)
    {
        floorOb[x, y] = floor;
    }
}
