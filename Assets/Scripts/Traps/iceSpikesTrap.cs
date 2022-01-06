using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceSpikesTrap : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    private GameObject[] spikes = new GameObject[4];

    private float TimePassed = 0;
    void Start()
    {
        SpawnSpikes();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimePassed > 1)
        {
            TimePassed = 0;
            ReleaseSpikes();
            SpawnSpikes();
        }
        else
        {
            TimePassed += Time.deltaTime;
        }
    }

    private void SpawnSpikes()
    {
        spikes[0] = Instantiate(spike, transform.position + new Vector3(0, 1, 1), Quaternion.identity);
        spikes[1] = Instantiate(spike, transform.position + new Vector3(0, -1, 1), Quaternion.identity);
        spikes[1].transform.Rotate(0, 0, 180, Space.Self);
        spikes[2] = Instantiate(spike, transform.position + new Vector3(-1, 0, 1), Quaternion.identity);
        spikes[2].transform.Rotate(0, 0, 90, Space.Self);
        spikes[3] = Instantiate(spike, transform.position + new Vector3(1, 0, 1), Quaternion.identity);
        spikes[3].transform.Rotate(0, 0, 270, Space.Self);
    }

    private void ReleaseSpikes()
    {
        for(int i = 0; i < 4; i++)
        {
            if(spikes[i] != null)
            {
                spikes[i].GetComponent<iceSpike>().Release = true;
            }
        }
    }
}
