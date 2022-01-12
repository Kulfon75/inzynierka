using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tar : MonoBehaviour
{
    private float speedReduction = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMenager>().speed /= speedReduction;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMenager>().speed *= speedReduction;
        }
    }
}
