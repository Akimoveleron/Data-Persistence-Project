using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField fieldName;
    public Text recordTextList;
    public Button startBttn;

    
    private void Start()
    {
       


        var se = new TMP_InputField.SubmitEvent();
        se.AddListener(SubmitName);
        fieldName.onEndEdit = se;
        UpdateRecordList();

    }
    private void SubmitName(string namePlayer)
    {
        if (!String.IsNullOrEmpty(namePlayer))
        {
            SaveRecords.instanceSaveRecord.namePlayer = namePlayer;
            var scorePlayer = SaveRecords.instanceSaveRecord.scorePlayer;
            if (!SaveRecords.instanceSaveRecord.recordList.ContainsKey(namePlayer))
            {
                SaveRecords.instanceSaveRecord.recordList.Add(namePlayer, scorePlayer);
                UpdateRecordList();

            }

            if (SaveRecords.instanceSaveRecord.namePlayer != null)
                startBttn.interactable = true;
            else
                startBttn.interactable = false;

        }

        

    }
    private void UpdateRecordList()
    {
      
       

        var records = SaveRecords.instanceSaveRecord.recordList;
        records = records.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        StringBuilder score = new StringBuilder();
        foreach (var item in records)
        {
            score.Append(item.Key + " " + item.Value + "\n");
        }
        recordTextList.text = score.ToString();
        SaveRecords.instanceSaveRecord.recordsText = score.ToString();
       
    }
    public void StartBttn()
    {

        SceneManager.LoadScene(1);
    }
    public void ExitBttn()
    {
        SaveRecords.instanceSaveRecord.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void SaveClick()
    {
        SaveRecords.instanceSaveRecord.Save();
    }
    public void LoadClick()
    {
        SaveRecords.instanceSaveRecord.Load();
        recordTextList.text = SaveRecords.instanceSaveRecord.recordsText;
    }
    public void RemoveFailClick()
    {
        SaveRecords.instanceSaveRecord.RemoveFail();
    }
}
