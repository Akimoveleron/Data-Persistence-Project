using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SaveRecords : MonoBehaviour
{
    public static SaveRecords instanceSaveRecord;   
    public string namePlayer;
    public int scorePlayer=0;
    public Dictionary<string, int> recordList = new Dictionary<string, int>();
    public string recordsText;
    public Text recordText;
    
    
    private void Awake()
    {
        
        
        if (instanceSaveRecord != null)
        {
            Destroy(gameObject);
            
        }
        else
        {
            instanceSaveRecord = this;
            DontDestroyOnLoad(gameObject);
           
        }
        StartCoroutine(WaitLoad());
        

    }
    [System.Serializable]
    class SaveData
    {
        public string recordsText;
      
    }
    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.recordsText = recordsText;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("SAVE");
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            recordsText = saveData.recordsText;
            recordText.text = recordsText;
            string [] splitText = recordsText.Split();
            for (int i = 1; i < splitText.Length; i+=2)
            {
                recordList.Add(splitText[i-1],int.Parse(splitText[i]) );
            }
            Debug.Log("LOAD");
        }

    }
    public void RemoveFail()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            File.Delete(Application.persistentDataPath + "/savefile.json");
            recordsText = " ";
            Debug.Log("Data reset complete!");
        }
    }

    private IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(0);
        Load();
    }
}
