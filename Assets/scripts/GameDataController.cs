using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataController : MonoBehaviour
{
    public GameObject player;
    public string saveFileRoute;
    public GameData gameData = new GameData();

    private void Awake()
    {
        saveFileRoute = Application.dataPath + "/Data/gameData.json";
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        bool isNew = (PlayerPrefs.GetInt("isNew") != 0);
        if (isNew)
        {
            DeleteData();
        }
        else
        {
            LoadData();
        }

    }
    private void Update()
    {

    }

    public void LoadData()
    {
        if (File.Exists(saveFileRoute))
        {
            string data = File.ReadAllText(saveFileRoute);
            gameData = JsonUtility.FromJson<GameData>(data);
            Debug.Log("pos: " + gameData.position);

            player.transform.position = gameData.position;
        }
        else
        {
            Debug.Log("No save data found");
        }
    }
    public void SaveData()
    {
        Debug.Log(player.transform.position);
        GameData newData = new GameData()
        {
            position = player.transform.position
        };
        string JSONstring = JsonUtility.ToJson(newData);

        File.WriteAllText(saveFileRoute, JSONstring);
        Debug.Log("File Saved");
    }

    public void DeleteData()
    {
        File.Delete(saveFileRoute);
    }

}
