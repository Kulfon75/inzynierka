using UnityEngine;

public class mouse_block : MonoBehaviour
{
    private Color block_color;
    private Color start_block_color;
    public int placeX;
    public int placeY;
    public bool colide = false;
    private bool placed = false;
    public GameObject hud;
    public GameObject Wall;
    private Vector3 position;
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("Hud");
        start_block_color = GetComponent<SpriteRenderer>().color;
        block_color = GetComponent<SpriteRenderer>().color;
        if(colide)
        {
            PlaceWallsDirections();
        }
        placed = true;
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
    public void PlaceWallsDirections()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (hud.GetComponentInChildren<place_room>().isTaken[placeX - 1, placeY])
        {
            PlaceWalls(0, true);
            if(!placed)
            {
                hud.GetComponentInChildren<place_room>().floorOb[placeX - 1, placeY].GetComponent<mouse_block>().PlaceWallsDirections();
            }
        }
        else
        {
            PlaceWalls(0, false);
        }
        if (hud.GetComponentInChildren<place_room>().isTaken[placeX, placeY + 1])
        {
            PlaceWalls(1, true);
            if (!placed)
            {
                hud.GetComponentInChildren<place_room>().floorOb[placeX, placeY + 1].GetComponent<mouse_block>().PlaceWallsDirections();
            }
        }
        else
        {
            PlaceWalls(1, false);
        }
        if (hud.GetComponentInChildren<place_room>().isTaken[placeX + 1, placeY])
        {
            PlaceWalls(2, true);
            if (!placed)
            {
                hud.GetComponentInChildren<place_room>().floorOb[placeX + 1, placeY].GetComponent<mouse_block>().PlaceWallsDirections();
            }
        }
        else
        {
            PlaceWalls(2, false);
        }
        if (hud.GetComponentInChildren<place_room>().isTaken[placeX, placeY - 1])
        {
            PlaceWalls(3, true);
            if (!placed)
            {
                hud.GetComponentInChildren<place_room>().floorOb[placeX, placeY - 1].GetComponent<mouse_block>().PlaceWallsDirections();
            }
        }
        else
        {
            PlaceWalls(3, false);
        }
    }
    public void PlaceWalls(int wallNo, bool wallType)
    {
        if (colide)
        {
            position = new Vector3(1, 1, -1);
            GameObject foo = Instantiate(Wall, position, Quaternion.identity);
            foo.transform.SetParent(this.GetComponent<Transform>());
            if(!wallType)
            {
                switch (wallNo)
                {
                    case 0:
                        foo.transform.localScale = new Vector3(0.1f, 1, 1);
                        foo.transform.localPosition = new Vector3(-0.45f, 0, -1);
                        break;
                    case 1:

                        foo.transform.localScale = new Vector3(1, 0.1f, 1);
                        foo.transform.localPosition = new Vector3(0, 0.45f, -1);
                        break;
                    case 2:
                        foo.transform.localScale = new Vector3(0.1f, 1, 1);
                        foo.transform.localPosition = new Vector3(0.45f, 0, -1);
                        break;
                    case 3:
                        foo.transform.localScale = new Vector3(1, 0.1f, 1);
                        foo.transform.localPosition = new Vector3(0, -0.45f, -1);
                        break;
                }
            }
            else
            {
                GameObject foo2 = Instantiate(Wall, position, Quaternion.identity);
                foo2.transform.SetParent(this.GetComponent<Transform>());
                switch (wallNo)
                {
                    case 0:
                        foo.transform.localScale = new Vector3(0.1f, 0.40f, 1);
                        foo.transform.localPosition = new Vector3(-0.45f, -0.30f, -1);
                        foo2.transform.localScale = new Vector3(0.1f, 0.40f, 1);
                        foo2.transform.localPosition = new Vector3(-0.45f, 0.30f, -1);
                        break;
                    case 1:
                        foo.transform.localScale = new Vector3(0.40f, 0.1f, 1);
                        foo.transform.localPosition = new Vector3(-0.30f, 0.45f, -1);
                        foo2.transform.localScale = new Vector3(0.40f, 0.1f, 1);
                        foo2.transform.localPosition = new Vector3(0.30f, 0.45f, -1);
                        break;
                    case 2:
                        foo.transform.localScale = new Vector3(0.1f, 0.40f, 1);
                        foo.transform.localPosition = new Vector3(0.45f, -0.30f, -1);
                        foo2.transform.localScale = new Vector3(0.1f, 0.40f, 1);
                        foo2.transform.localPosition = new Vector3(0.45f, 0.30f, -1);
                        break;
                    case 3:
                        foo.transform.localScale = new Vector3(0.40f, 0.1f, 1);
                        foo.transform.localPosition = new Vector3(-0.30f, -0.45f, -1);
                        foo2.transform.localScale = new Vector3(0.40f, 0.1f, 1);
                        foo2.transform.localPosition = new Vector3(0.30f, -0.45f, -1);
                        break;
                }
            }
        }
    }
}
