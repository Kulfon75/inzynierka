using System;
using UnityEngine;

public class create_grid : MonoBehaviour
{
    public Transform block;
    public Canvas hud;
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        createGrid(10, 10, 20);
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
                if(i == x - 1 && j == enemy_spawn_pos)
                {
                    block.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
                    block.name = "boss_chamber";
                }
                else
                {
                    if (i == 0 && j == boss_chamber_pos)
                    {
                        block.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0); 
                        block.name = "enemy_spawn";
                    }
                    else
                    {
                        block.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                        block.name = "floor";
                    }
                }
                Transform floor = Instantiate(block, new Vector3(i * width, j * width, 1), Quaternion.identity);
                floor.GetComponent<mouse_block>().placeX = i + 1;
                floor.GetComponent<mouse_block>().placeY = j + 1;
            }
        }
        hud.GetComponentInChildren<place_room>().setTakenFalse(x,y);
        hud.GetComponent<hud>().block = GameObject.Find("boss_chamber(Clone)");
        hud.GetComponentInChildren<place_room>().OnPointerDown(null);
        hud.GetComponent<hud>().block = GameObject.Find("enemy_spawn(Clone)");
        hud.GetComponentInChildren<place_room>().OnPointerDown(null);
    }
}
