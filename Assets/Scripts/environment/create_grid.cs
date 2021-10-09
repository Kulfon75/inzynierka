using System;
using UnityEngine;

public class create_grid : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    System.Random rand = new System.Random();
    private int width;
    private int height;
    private int roomSize;
    private int[,] gridArray;
    private TextMesh[,] debug;
    void Start()
    {
        width = 10;
        height = 10;
        roomSize = 20;
        createGrid(width, height, roomSize);
        //createGridPathFinder();
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
                floor.GetComponent<mouse_block>().placeX = i + 1;
                floor.GetComponent<mouse_block>().placeY = j + 1;
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
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("boss_chamber").GetComponent<mouse_block>().placeX, GameObject.Find("boss_chamber").GetComponent<mouse_block>().placeY, GameObject.Find("boss_chamber"));
        hud.GetComponentInChildren<place_room>().PlaceRoomOnPosition(GameObject.Find("enemy_spawn").GetComponent<mouse_block>().placeX, GameObject.Find("enemy_spawn").GetComponent<mouse_block>().placeY, GameObject.Find("enemy_spawn"));
    }

    public void createGridPathFinder()
    {
        gridArray = new int[width * roomSize / 2, height * roomSize / 2];
        debug = new TextMesh[width * roomSize / 2, height * roomSize / 2];
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debug[x, y] = GetComponentInParent<create_text>().CreateWordText(this.transform, gridArray[x, y].ToString(), new Vector3(x * 2 - roomSize / 2 + 1, y * 2 - roomSize / 2 + 1, 1.0f), 20, Color.red, TextAnchor.MiddleCenter);
            }
        }
    }

    private void setValue()
    {
        gridArray[5, 5] = 8;
        debug[5, 5].text = 8.ToString();
    }
}
