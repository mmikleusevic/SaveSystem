using System;
using System.IO;
using UnityEngine;

public class JsonSave : MonoBehaviour
{
    public static JsonSave Instance { get; private set; }
    
    [SerializeField] private Data data = new Data();
    
    private string filePath;

    private void Awake()
    {
        Instance = this;
        filePath = Application.persistentDataPath + "/data.json";
    }

    private void Start()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
            
            Debug.Log("File created at: " + filePath);
        }
        else
        {
            Load();
        }
    }

    public void Save()
    {
        if (data == null)
        {
            Debug.LogError("Data is null");

            return;
        }
        
        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(filePath, json);
        
        Debug.Log("Data saved");
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found");
            
            return;
        }
        
        string json = File.ReadAllText(filePath);
        
        data = JsonUtility.FromJson<Data>(json);
        
        Debug.Log("Data loaded: " + json);
    }
    
    public void SetScore(int scoreAmount)
    {
        data.score = scoreAmount;
    }
    
    public void SetName(string playerName)
    {
        data.playerName = playerName;
    }

    public void SetPosition(Vector3 position)
    {
        data.position = position;
    }

    public void SetPassword(string password)
    {
        data.password = password;
    }

    public void SetPositions(Vector3[] positions)
    {
        data.positions = positions;
    }

    public int GetScore()
    {
        Load();
        
        return data.score;
    }
    
    public string GetName()
    {
        Load();
        
        return data.playerName;
    }

    public Vector3 GetPosition()
    {
        Load();
        
        return data.position;
    }

    public string GetPassword()
    {
        Load();
        
        return data.password;
    }

    public Vector3[] GetPositions()
    {
        Load();
        
        return data.positions;
    }

    public bool CheckPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            Debug.LogError("Password is empty");
            return false;
        }
        else if (string.IsNullOrEmpty(data.password))
        {
            Debug.LogError("Data password is empty");
            return false;
        }
        else if (data.password == password)
        {
            return true;
        }

        return false;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[Serializable]
public class Data
{
    public int score;
    public string playerName;
    public Vector3 position;
    public string password;
    public Vector3[] positions;
}