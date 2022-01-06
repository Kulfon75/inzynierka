using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMenager : MonoBehaviour
{
    public float hp;
    public int speed;
    public int rewardAfterKill;
    public float damage;
    private playerStats PlStats;
    private spawnRoomMenager SpRoomMen;

    private void Start()
    {
        SpRoomMen = GameObject.Find("enemy_spawn").GetComponent<spawnRoomMenager>();
        PlStats = GameObject.FindGameObjectWithTag("Hud").GetComponent<playerStats>();
    }
    public void UpdateHp(float damageTaken, bool isBossRoom)
    {
        hp -= damageTaken;
        if(hp <= 0)
        {
            PlStats.UpdateMoney(rewardAfterKill);
            SpRoomMen.enemyCouneter--;
            Destroy(this.gameObject);
        }
        if (isBossRoom)
        {
            SpRoomMen.enemyCouneter--;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "boss_chamber")
        {
            PlStats.UpdateHp(damage);
            UpdateHp(0, true);
        }
    }
}
