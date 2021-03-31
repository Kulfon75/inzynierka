using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_grid : MonoBehaviour
{
    public Transform block;
    public Transform block_collision;
    private int[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        createGrid(10, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void createGrid(int x, int y)
    {
        for(int i = 0; i <= x; i++)
        {
            for(int j = 0; j <= y; j++)
            {
                if(i == 0 || j == 0 || i == x || j == y)
                {
                    Instantiate(block_collision, new Vector3(i * 2.0f, j * 2.0f, 1), Quaternion.identity);
                }
                else
                {
                    Instantiate(block, new Vector3(i * 2.0f, j * 2.0f, 1), Quaternion.identity);
                }    
            }
        }
    }
}
