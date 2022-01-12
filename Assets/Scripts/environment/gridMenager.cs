using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class gridMenager : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    private playerStats PlStats;
    private hud hudScr;
    System.Random rand = new System.Random();
    private int width;
    private int height;
    private int roomSize;
    private RaycastHit2D hit;
    private pathFinding PathFind;
    private bool debug = false;
    public bool generated;
    void Start()
    {
        width = 5;
        height = 5;
        roomSize = 20;
        generated = false;
        hudScr = hud.GetComponent<hud>();
        PlStats = hud.GetComponent<playerStats>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && generated)
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

        if (Input.GetMouseButtonDown(1) && generated)
        {
            Vector3 mouseWorldPos = GetMousePos();
            PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
        }

        if (Input.GetKeyDown(KeyCode.V) && generated)
        {
            for(int i = 1; i < (width * 10) * 2; i += 2)
            {
                for (int j = 1; j < (height * 10) * 2; j += 2)
                {
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(i + (int)PathFind.GetGrid().originPos.x, j + (int)PathFind.GetGrid().originPos.y), -Vector2.up);
                    PathFind.GetGrid().GetXY(new Vector3(i + (int)PathFind.GetGrid().originPos.x, j + (int)PathFind.GetGrid().originPos.y), out int x, out int y);
                    if (hit.collider.tag == "Wall" || (PathFind.GetNode(x, y).isTrap && !PathFind.GetNode(x, y).isTrapWalkable))
                    {
                        PathFind.GetNode(x, y).SetIsWalkable(false);
                        if(hit.collider.tag == "Wall")
                            PathFind.GetNode(x, y).isWall = true;
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
                        if ((PathFind.GetNode(x - 1, y).isTrap || PathFind.GetNode(x, y - 1).isTrap) && !PathFind.GetNode(x, y).isTrap && !PathFind.GetNode(x, y).isWall)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                    }
                    if (x + 1 < (height * 10) && y + 1 < (height * 10))
                    {
                        if ((PathFind.GetNode(x + 1, y).isTrap || PathFind.GetNode(x, y + 1).isTrap) && !PathFind.GetNode(x, y).isTrap && !PathFind.GetNode(x, y).isWall)
                        {
                            PathFind.GetNode(x, y).SetIsWalkable(true);
                        }
                    }
                    if(PathFind.GetNode(x, y).isTrapWalkable)
                    {
                        PathFind.GetNode(x, y).SetIsWalkable(true);
                    }
                    PathFind.GetGrid().SetGridObject(x, y, PathFind.GetNode(x, y));
                }
            }
        }
    }

    public void createGrid(int x, int y, int roomSize)
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
                Transform floor = Instantiate(block, new Vector3(i * roomSize, j * roomSize, 3), Quaternion.identity);
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
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("boss_chamber").GetComponent<roomMenager>().placeX, GameObject.Find("boss_chamber").GetComponent<roomMenager>().placeY, GameObject.Find("boss_chamber"));
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("enemy_spawn").GetComponent<roomMenager>().placeX, GameObject.Find("enemy_spawn").GetComponent<roomMenager>().placeY, GameObject.Find("enemy_spawn"));
        PathFind = new pathFinding(width, height, transform);
    }

    public void SetTrap()
    {
        if (hudScr.isChecked && hudScr.checkType > 2 && !PlStats.PlayWave)
        {
            if(hudScr.cost < PlStats.money)
            {
                Vector3 mouseWorldPos = GetMousePos();
                PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
                if (x >= 0 && y >= 0 && x < (width * 10) && y < (height * 10) && !PathFind.GetNode(x, y).isTrap && !PathFind.GetNode(x, y).isWall)
                {
                    PlStats.UpdateMoney(-hudScr.cost);
                    PathFind.GetNode(x, y).SetIsTrap(true);
                    if(hudScr.checkType > 3)
                    {
                        PathFind.GetNode(x, y).isTrapWalkable = true;
                    }
                    Instantiate(hudScr.block, PathFind.GetGrid().GetPosition(x, y) + new Vector3(1, 1), Quaternion.identity);
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
