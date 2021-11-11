using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveRecords : MonoBehaviour
{
    public static SaveRecords instanceSaveRecord;   
    public string namePlayer;
    public int scorePlayer=0;
    public Dictionary<string, int> recordList = new Dictionary<string, int>();
    private void Start()
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
        
    }
}
