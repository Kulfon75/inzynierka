using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceSpike : MonoBehaviour
{
    public bool Release = false;
    private int damage = 5;
    private Rigidbody2D RBody;

    void Start()
    {
        RBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Release)
        {
            transform.position += transform.up * 15 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<enemyMenager>().UpdateHp(damage, false);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Wall" || collision.tag == "Trap")
        {
            Destroy(this.gameObject);
        }
    }
}
