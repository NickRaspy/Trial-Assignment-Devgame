using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScoreRecord : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Record: " + ScoreRecord.GetRecord();        
    }
}
