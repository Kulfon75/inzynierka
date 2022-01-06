using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{

    private int damage = 10;
    public int cost = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMenager>().UpdateHp(damage, false);
        }
    }
}
