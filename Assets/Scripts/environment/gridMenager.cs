using System;
using UnityEngine;
using System.Collections.Generic;

public class gridMenager : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    private hud hudScr;
    System.Random rand = new System.Random();
    private int width;
    private int height;
    private int roomSize;
    private RaycastHit2D hit;
    private pathFinding PathFind;
    private bool debug = false;
    void Start()
    {
        width = 3;
        height = 3;
        roomSize = 20;
        createGrid(width, height, roomSize);
        PathFind = new pathFinding(width, height, transform);
        hudScr = hud.GetComponent<hud>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (debug)
            {
                Vector3 mouseWorldPos = GetMousePos();
                PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
                List<PathNode> path = PathFind.FindPath(1, 1, x, y);
                if (path != null)
                {
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 2f + Vector3.one * -9, new Vector3(path[i + 1].x, path[i + 1].y) * 2f + Vector3.one * -9, Color.green, 5f);
                    }
                }
            }
            SetTrap();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = GetMousePos();
            PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            for(int i = 1; i < (width * 10) * 2; i += 2)
            {
                for (int j = 1; j < (height * 10) * 2; j += 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(i + (int)PathFind.GetGrid().originPos.x, j + (int)PathFind.GetGrid().originPos.y), -Vector2.up);
                    PathFind.GetGrid().GetXY(new Vector3(i + (int)PathFind.GetGrid().originPos.x, j + (int)PathFind.GetGrid().originPos.y), out int x, out int y);
                    if (hit.collider.tag == "Wall" || PathFind.GetNode(x, y).isTrap)
                    {
                        PathFind.GetNode(x, y).SetIsWalkable(false);
                    }
                    else
                    {
                        if (((i - 1) / 2) % 5 == 0 || ((i + 1) / 2) % 5 == 0)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                        if (((j - 1) / 2) % 5 == 0 || ((j + 1) / 2) % 5 == 0)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                    }
                    if (x - 1 > 0 && y - 1 > 0)
                    {
                        if ((PathFind.GetNode(x - 1, y).isTrap || PathFind.GetNode(x, y - 1).isTrap) && !PathFind.GetNode(x, y).isTrap)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                    }
                    if(x + 1 < (height * 10) && y + 1 < (height * 10))
                    {
                        if ((PathFind.GetNode(x + 1, y).isTrap || PathFind.GetNode(x, y + 1).isTrap) && !PathFind.GetNode(x, y).isTrap)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                    }
                    PathFind.GetGrid().SetGridObject(x, y, PathFind.GetNode(x, y));
                }
            }
        }
    }

    public void createGrid(int x, int y, int width)
    {
        int enemy_spawn_pos = rand.Next(0, y - 1);
        int boss_chamber_pos = 0;
        while(enemy_spawn_pos == boss_chamber_pos)
        {
            boss_chamber_pos = rand.Next(0, y - 1);
        }
        for (int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                Transform floor = Instantiate(block, new Vector3(i * width, j * width, 1), Quaternion.identity);
                floor.SetParent(this.transform);
                floor.GetComponent<roomMenager>().placeX = i + 1;
                floor.GetComponent<roomMenager>().placeY = j + 1;
                if (i == x - 1 && j == enemy_spawn_pos)
                {
                    floor.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                    floor.name = "boss_chamber";
                }
                else
                {
                    if (i == 0 && j == boss_chamber_pos)
                    {
                        floor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0);
                        floor.name = "enemy_spawn";
                    }
                    else
                    {
                        floor.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                        floor.name = "floor";
                    }
                } 
            }
        }
        hud.enabled = true;
        hud.GetComponentInChildren<place_room>().setTakenFalse(x,y);
        /*hud.GetComponent<hud>().block = GameObject.Find("boss_chamber(Clone)");
        hud.GetComponentInChildren<place_room>().OnPointerDown(null);
        hud.GetComponent<hud>().block = GameObject.Find("enemy_spawn");
        hud.GetComponentInChildren<place_room>().OnPointerDown(null);*/
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("boss_chamber").GetComponent<roomMenager>().placeX, GameObject.Find("boss_chamber").GetComponent<roomMenager>().placeY, GameObject.Find("boss_chamber"));
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("enemy_spawn").GetComponent<roomMenager>().placeX, GameObject.Find("enemy_spawn").GetComponent<roomMenager>().placeY, GameObject.Find("enemy_spawn"));
    }

    public void SetTrap()
    {
        if (hudScr.isChecked && hudScr.checkType > 2)
        {
            Vector3 mouseWorldPos = GetMousePos();
            PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
            if (x >= 0 && y >= 0  && x < (width * 10) && y < (height * 10) && PathFind.GetNode(x, y).isWalkable)
            {
                PathFind.GetNode(x, y).SetIsTrap(true);
                Instantiate(hudScr.block, PathFind.GetGrid().GetPosition(x, y) + new Vector3(1, 1), Quaternion.identity);
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
