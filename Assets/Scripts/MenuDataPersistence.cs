using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuDataPersistence : MonoBehaviour
{
    // Start is called before the first frame update

    public static MenuDataPersistence instance;
    public string playerName, currentPlayer;
    public int highPoint;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);

        LoadName();
    }

    [System.Serializable]
    class PlayerName
    {
        public string player;
        public int hScore;
    }

    public void SaveName()
    {
        PlayerName data = new PlayerName();
        data.player = playerName;
        data.hScore = MainDataPersistence.instance.highScore;

        Debug.Log(data.hScore);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerName data = JsonUtility.FromJson<PlayerName>(json);

            playerName = data.player;
            highPoint = data.hScore;
        }
    }
}
