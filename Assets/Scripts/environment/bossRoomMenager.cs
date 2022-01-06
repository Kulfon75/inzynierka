using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossRoomMenager : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = transform.position + new Vector3(0, 0, -2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
