using System;
using UnityEngine;
using System.Collections.Generic;

public class create_grid : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    System.Random rand = new System.Random();
    private int width;
    private int height;
    private int roomSize;


    private pathFinding PathFind;
    void Start()
    {
        width = 3;
        height = 3;
        roomSize = 20;
        createGrid(width, height, roomSize);
        PathFind = new pathFinding(width, height, transform);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = GetMousePos();
            PathFind.GetGrid().GetXY(mouseWorldPos, out int x, out int y);
            List<PathNode> path = PathFind.FindPath(0, 0, x, y);
            if(path != null)
            {
                for(int i = 0; i < path.Count - 1; i++)
                {
                    Debug.Log(x + " " + y);
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 2f + Vector3.one * -9, new Vector3(path[i + 1].x, path[i + 1].y) * 2f + Vector3.one * -9, Color.green, 5f);
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

    public Vector3 GetMousePos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0;
        return vec;
    }
}
