using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    private playerStats PlStats;
    private gridMenager GrMen;
    private place_room PlRoom;
    [SerializeField] private GameObject TrapHolder;
    [SerializeField] private GameObject BoardHolder;

    [SerializeField] private GameObject[] Traps = new GameObject[4];
    private void Start()
    {
        PlStats = GameObject.Find("HUD").GetComponent<playerStats>();
        GrMen = GameObject.Find("Tilemap").GetComponent<gridMenager>();
        PlRoom = GameObject.FindGameObjectWithTag("Menu").GetComponent<place_room>();
        TrapHolder = GameObject.Find("Traps");
        BoardHolder = GameObject.Find("Tilemap");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Load();
        }
    }
    public void Save()
    {
        FileStream SaveFile = File.Create(Application.persistentDataPath + "/saveInfo.data");
        Data SaveData = new Data();
        SaveData.money = PlStats.money;
        SaveData.HP = PlStats.HP;
        SaveData.wave = PlStats.wave;
        SaveData.BoardX = GrMen.width;
        SaveData.BoardY = GrMen.height;
        SaveData.IsWalled = PlRoom.isTaken;
        SaveData.BossChamberPosX = PlRoom.bossChposX;
        SaveData.BossChamberPosY = PlRoom.bossChposY;
        SaveData.SpawnChamberPosX = PlRoom.enemySpaposX;
        SaveData.SpawnChamberPosY = PlRoom.enemySpaposY;
        SaveData.TrapType = new int[TrapHolder.transform.childCount + 1];
        SaveData.TrapPosX = new float[TrapHolder.transform.childCount + 1];
        SaveData.TrapPosY = new float[TrapHolder.transform.childCount + 1];
        SaveData.TrapPosZ = new float[TrapHolder.transform.childCount + 1];
        int i = 0;
        foreach(Transform t in TrapHolder.transform)
        {
            SaveData.TrapPosX[i] = t.position.x;
            SaveData.TrapPosY[i] = t.position.y;
            SaveData.TrapPosZ[i] = t.position.z;
            switch(t.name)
            {
                case "Rock(Clone)":
                    SaveData.TrapType[i] = 0;
                    break;
                case "Spikes(Clone)":
                    SaveData.TrapType[i] = 1;
                    break;
                case "IceSpikesTrap(Clone)":
                    SaveData.TrapType[i] = 2;
                    break;
                case "Tar(Clone)":
                    SaveData.TrapType[i] = 3;
                    break;
                default:
                    break;
            }
            i++;
        }
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(SaveFile, SaveData);
        SaveFile.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveInfo.data"))
        {
            GrMen.IsLoad = true;
            FileStream SaveFile = File.Open(Application.persistentDataPath + "/saveInfo.data", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Data SaveData = (Data)bf.Deserialize(SaveFile);
            SaveFile.Close();
            PlStats.money = SaveData.money;
            PlStats.UpdateMoney(0);
            PlStats.HP = SaveData.HP;
            PlStats.UpdateHp(0);
            PlStats.wave = SaveData.wave - 1;
            PlStats.UpdateWave();
            PlRoom.bossChposX = SaveData.BossChamberPosX;
            PlRoom.bossChposY = SaveData.BossChamberPosY;
            PlRoom.enemySpaposX = SaveData.SpawnChamberPosX;
            PlRoom.enemySpaposY = SaveData.SpawnChamberPosY;
            foreach (Transform t in BoardHolder.transform)
            {
                Destroy(t.gameObject);
            }
            GrMen.createGrid(SaveData.BoardX, SaveData.BoardY, 20);
            for(int i = 0; i < SaveData.BoardX; i++)
            {
                for (int j = 0; j < SaveData.BoardY; j++)
                {
                    if(SaveData.IsWalled[i, j])
                    {
                       PlRoom.PlaceRoomOnPosition(i, j, PlRoom.floorOb[i, j]);
                    }
                }
            }
            foreach (Transform t in TrapHolder.transform)
            {
                Destroy(t.gameObject);
            }
            for (int i = 0; i < SaveData.TrapType.Length; i++)
            {
                GrMen.LoadTrapPos.x = SaveData.TrapPosX[i];
                GrMen.LoadTrapPos.y = SaveData.TrapPosY[i];
                GrMen.LoadTrapPos.z = SaveData.TrapPosZ[i];
                GrMen.TrapObjLoad = Traps[SaveData.TrapType[i]];
                GrMen.SetTrap();
            }
            GrMen.IsLoad = false;
        }
    }
}

[Serializable]
class Data
{
    public int money;
    public float HP;
    public int wave;

    public int BoardX;
    public int BoardY;
    public bool[,] IsWalled;

    public int BossChamberPosX;
    public int BossChamberPosY;

    public int SpawnChamberPosX;
    public int SpawnChamberPosY;

    [SerializeField] public int[] TrapType;
    [SerializeField] public float[] TrapPosX;
    [SerializeField] public float[] TrapPosY;
    [SerializeField] public float[] TrapPosZ;
}

