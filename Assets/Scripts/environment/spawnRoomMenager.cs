using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRoomMenager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private enemyMenager EnMen;
    private playerStats PlStats;
    public int enemyCouneter;
    private float TimerSpawn;
    private float WaveTimer;
    [SerializeField] private place_room plRoom;
    private void Start()
    {
        WaveTimer = 0;
        enemyCouneter = 0;
        PlStats = GameObject.FindGameObjectWithTag("Hud").GetComponent<playerStats>();
        PlStats.PlayWave = false;
        plRoom = GameObject.FindGameObjectWithTag("Menu").GetComponent<place_room>();
    }
    void Update()
    {
        if (PlStats.PlayWave)
        {
            if (TimerSpawn > Random.Range(0.5f, 5))
            {
                SpawnEnemy();
                TimerSpawn = 0;
            }
            else
            {
                TimerSpawn += Time.deltaTime;
            }

            if (WaveTimer > 60)
            {
                PlStats.UpdateWave();
                WaveTimer = 0;
                PlStats.PlayWave = false;
            }
            else
            {
                WaveTimer += Time.deltaTime;
            }
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            plRoom.IsPath = false;
            plRoom.checkPath(plRoom.enemySpaposX, plRoom.enemySpaposY, plRoom.bossChposX, plRoom.bossChposY);
            if(plRoom.IsPath)
            PlStats.PlayWave = true;
        }
    }

    private void SpawnEnemy()
    {
        if(enemyCouneter < 10)
        {
            EnMen = Instantiate(enemy, transform.position + new Vector3(0, 0, -3), Quaternion.identity).GetComponent<enemyMenager>();
            EnMen.damage = PlStats.wave * 10;
            EnMen.hp = PlStats.wave * 10;
            EnMen.rewardAfterKill = PlStats.wave * 3;
            enemyCouneter++;
        }
    }
}
