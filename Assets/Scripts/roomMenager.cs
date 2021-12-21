using UnityEngine;

public class roomMenager : MonoBehaviour
{
    private Color start_block_color;
    public int placeX, placeY;
    public bool colide = false;
    private bool placed = false;
    public GameObject hud, Wall;
    private hud hudVariables;
    private Vector3 position;
    private Vector4 oldColor;
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("Hud");
        hudVariables = hud.GetComponent<hud>();
        start_block_color = GetComponent<SpriteRenderer>().color;
        oldColor = GetComponent<SpriteRenderer>().color;
        if(colide)
        {
            PlaceWallsDirections();
        }
        placed = true;
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Vector4(oldColor.x, oldColor.y, oldColor.z, 0.5f);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = oldColor;
    }

    private void OnMouseDown()
    {
        if (hudVariables.checkType == 1)
        {
            hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(placeX, placeY, this.gameObject);
        }
        if (hudVariables.checkType == 2)
        {
            hud.GetComponentInChildren<place_room>().destroyRoom(placeX, placeY, this.gameObject);
        }
    }

    public void IsActive(bool is_active)
    {
        if(!is_active)
        {
            GetComponent<SpriteRenderer>().color = oldColor;
        }
        else
        {
            oldColor = Color.blue;
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
                hud.GetComponentInChildren<place_room>().floorOb[placeX - 1, placeY].GetComponent<roomMenager>().PlaceWallsDirections();
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
                hud.GetComponentInChildren<place_room>().floorOb[placeX, placeY + 1].GetComponent<roomMenager>().PlaceWallsDirections();
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
                hud.GetComponentInChildren<place_room>().floorOb[placeX + 1, placeY].GetComponent<roomMenager>().PlaceWallsDirections();
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
                hud.GetComponentInChildren<place_room>().floorOb[placeX, placeY - 1].GetComponent<roomMenager>().PlaceWallsDirections();
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

    public Vector3 GetMousePos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
