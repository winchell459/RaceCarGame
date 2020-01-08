using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    public int TotalLaps = 3;
    private int currentLap = 1;

    private float raceStartTime = 0;
    private bool raceStarted = false;
    private bool raceEnded = false;

    public Text TimeText;
    public Text CurrentLapText;
    public Text RaceText;

    public BoxCollider[] Checkpoints;
    private int currentCheckpoint = 0;

	// Use this for initialization
	void Start () {
        startRace();
        RaceText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(!raceStarted){
            if(Input.GetKeyDown(KeyCode.W)){
                raceStarted = true;
                raceStartTime = Time.fixedTime;
            }
        }else if(!raceEnded){
            updateTimeText();
        }

	}

    void updateTimeText(){
        float time = Time.fixedTime - raceStartTime;
        int min = (int)(time / 60);
        int sec = (int)(time - min * 60);
        int mil = (int)((time - sec - min * 60) * 10);

        string strTime = "";
        if (min < 10) strTime += "0";
        strTime += min;
        strTime += ":";
        if (sec < 10) strTime += "0";
        strTime += sec;
        strTime += ".";
        strTime += mil;

        TimeText.text = strTime;
    }

    void startRace(){
        foreach(BoxCollider bc in Checkpoints){
            bc.gameObject.SetActive(false);
        }
        Checkpoints[currentCheckpoint].gameObject.SetActive(true);
    }

    public void CheckpointTriggered(GameObject checkpoint){
        if(checkpoint == Checkpoints[currentCheckpoint].gameObject){
            if(currentCheckpoint >= Checkpoints.Length - 1){
                CheckLap();
            }else{
                Checkpoints[currentCheckpoint].gameObject.SetActive(false);
                currentCheckpoint += 1;
                Checkpoints[currentCheckpoint].gameObject.SetActive(true);
            }
        }
    }

    void CheckLap(){
        if(currentLap < TotalLaps){
            currentLap += 1;
            CurrentLapText.text = currentLap.ToString();

            Checkpoints[currentCheckpoint].gameObject.SetActive(false);
            currentCheckpoint = 0;
            Checkpoints[currentCheckpoint].gameObject.SetActive(true);
        }else{
            Checkpoints[currentCheckpoint].gameObject.SetActive(false);
            raceEnded = true;
            RaceText.gameObject.SetActive(true);
        }
    }
}
