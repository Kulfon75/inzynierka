using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    private int movementSpeed = 5;
    public int velX = 0;
    public int velY = 0;
    private bool zmiana = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Room")
        {
            if (velX > 0)
            {
                velX = 0;
                velY = -movementSpeed;
            }
            else if(velX < 0)
            {
                velX = 0;
                velY = movementSpeed;
            }
            else
            {
                if (velY < 0)
                {
                    velY = 0;
                    velX = -movementSpeed;
                }
                else if (velY > 0)
                {
                    velY = 0;
                    velX = movementSpeed;
                }
            }
        }
    }
}
