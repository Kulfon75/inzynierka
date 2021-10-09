using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class place_room : MonoBehaviour, IPointerDownHandler
{
    public GameObject room;
    public GameObject plane;
    public GameObject enemy_spawn;
    public GameObject boss_chamber;
    private Vector3 position_on_board;
    public Grid position_in_array;
    private int pos_array_x;
    private int pos_array_y;
    public bool[,] isTaken;
    private bool[,] isVerified; 
    public GameObject[,] floorOb;
    public GameObject floor;
    [SerializeField] private int bossChposX, bossChposY, enemySpaposX, enemySpaposY;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            checkPath(enemySpaposX, enemySpaposY, bossChposX, bossChposY);
            ClearPath();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        /*if (GetComponentInParent<hud>().block != null)
        {
            pos_array_x = GetComponentInParent<hud>().block.GetComponent<mouse_block>().placeX;
            pos_array_y = GetComponentInParent<hud>().block.GetComponent<mouse_block>().placeY;
            isTaken[pos_array_x, pos_array_y] = true;
            position_on_board = GetComponentInParent<hud>().block.transform.position;
            Destroy(GetComponentInParent<hud>().block);
            if (GetComponentInParent<hud>().block.name == "boss_chamber")
            {
                floor = Instantiate(boss_chamber, position_on_board, Quaternion.identity);
            }
            else
            {
                if(GetComponentInParent<hud>().block.gameObject.name == "enemy_spawn")
                {
                    floor = Instantiate(enemy_spawn, position_on_board, Quaternion.identity);
                }
                else
                {
                    floor = Instantiate(room, position_on_board, Quaternion.identity);
                }
            }
            GetComponentInParent<hud>().block = null;
            floor.GetComponent<mouse_block>().placeX = pos_array_x;
            floor.GetComponent<mouse_block>().placeY = pos_array_y;
            floor.GetComponent<mouse_block>().colide = true;
            SetArrayValueFloor(pos_array_x, pos_array_y, floor.gameObject);
        }*/
    }

    public void setTakenFalse(int x, int y)
    {
        floorOb = new GameObject[x + 2, y + 2];
        isTaken = new bool[x + 2, y + 2];
        isVerified = new bool[x + 2, y + 2];
        for (int i = 0; i <= x + 1; i++)
        {
            for (int j = 0; j <= y + 1; j++)
            {
                isTaken[x, y] = false;
                isVerified[x, y] = false;
            }
        }
    }

    public void SetArrayValueFloor(int x, int y, GameObject floor)
    {
        floorOb[x, y] = floor;
    }

    public void PlaceRoomOnPosition(int floorX, int floorY, GameObject floorRef)
    {
        isTaken[floorX, floorY] = true;
        position_on_board = floorRef.transform.position;
        if(floorRef.name == "enemy_spawn")
        {
            floor = Instantiate(enemy_spawn, position_on_board, Quaternion.identity);
            floor.transform.SetParent(floorRef.transform.parent);
            floor.name = "enemy_spawn";
            enemySpaposX = floorX;
            enemySpaposY = floorY;
        }
        else if(floorRef.name == "boss_chamber")
        {
            floor = Instantiate(boss_chamber, position_on_board, Quaternion.identity);
            floor.transform.SetParent(floorRef.transform.parent);
            floor.name = "boss_chamber";
            bossChposX = floorX;
            bossChposY = floorY;
        }
        else
        {
            floor = Instantiate(room, position_on_board, Quaternion.identity);
            floor.transform.SetParent(floorRef.transform.parent);
        }
        Destroy(floorRef);
        floor.GetComponent<mouse_block>().placeX = floorX;
        floor.GetComponent<mouse_block>().placeY = floorY;
        floor.GetComponent<mouse_block>().colide = true;
        SetArrayValueFloor(floorX, floorY, floor.gameObject);
    }

    public void checkPath(int currentX, int currentY, int endingX, int endingY)
    {
         if(!(currentX == endingX && currentY == endingY))
         {
             if((currentX >= 0 && currentY >= 0) && (currentX <= isTaken.GetLength(0) && currentY <= isTaken.GetLength(1)))
             {
                 isVerified[currentX, currentY] = true;
                if (isTaken[currentX + 1, currentY] == true)
                {
                    if (isVerified[currentX + 1, currentY] == false)
                    {
                        checkPath(currentX + 1, currentY, endingX, endingY);
                    }
                }

                if (isTaken[currentX - 1, currentY] == true)
                {
                    if (isVerified[currentX - 1, currentY] == false)
                    {
                        checkPath(currentX - 1, currentY, endingX, endingY);
                    }
                }
                
                if (isTaken[currentX, currentY + 1] == true)
                {
                    if (isVerified[currentX, currentY + 1] == false)
                    {
                        checkPath(currentX, currentY + 1, endingX, endingY);
                    }
                }
                
                 if (isTaken[currentX, currentY - 1] == true)
                 {
                     if (isVerified[currentX, currentY - 1] == false)
                     {
                         checkPath(currentX, currentY - 1, endingX, endingY);
                     }
                 }
             }
         }
         else
         {
             Debug.Log("znaleziono");
         }
    }

    private void ClearPath()
    {
        for(int i = 0; i < isVerified.GetLength(0); i++)
        {
            for(int j = 0; j < isVerified.GetLength(1); j++)
            {
                isVerified[i, j] = false;
            }
        }
    }

    public void destroyRoom(int posX, int posY, GameObject blockRef)
    {
        if(blockRef.transform.tag == "Room")
        {
            isTaken[posX, posY] = false;
            position_on_board = blockRef.transform.position;
            floor = Instantiate(plane, position_on_board, Quaternion.identity);
            Destroy(blockRef);
            floor.GetComponent<mouse_block>().placeX = posX;
            floor.GetComponent<mouse_block>().placeY = posY;
            floor.GetComponent<mouse_block>().colide = false;
            SetArrayValueFloor(posX, posY, floor.gameObject);
        }
    }
}
