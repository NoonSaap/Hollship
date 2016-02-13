﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour {
    static public GameManger Instance;

    public enum Color
    {
        Red,
        Blue,
        White,
        Yellow,
    }

    //Component
    public CameraFollow follow;
    public Player player;

    //Logic
    public float score = 0;
    public bool gameOver = false;
    public float mapoffset = 0;
    public int maploopcnt = 0;
    public int blockper = 0;
    public List<MapBuilder> maplist = new List<MapBuilder>();

    //UI
    public UILabel lb_score;
    public GameObject GO_Pause;
    public GameObject GO_PauseBtn;
    public GameObject GO_GameOver;
    public GameObject GO_GameOverBtn;
    public UILabel lb_gameoverScore;

    void Awake()
    {
        Instance = this;
        UpdateUI();
        maplist.ForEach(x => x.SetData(blockper));
    }

    public void GameOver()
    {
        gameOver = true;
        lb_gameoverScore.text = string.Format("{0:0.0}", score);
        GO_GameOver.SetActive(true);

        //이게뭐야 ㅡㅡ
        GO_GameOverBtn.SetActive(false);
        GO_GameOverBtn.SetActive(true);
    }

    public bool CheckPlayerOnBlock()
    {
        for (int i = 0; i < maplist.Count; i++)
        {
            for (int j = 0; j < maplist[i].blocklist.Count; j++)
            {
                if (maplist[i].blocklist[j].ison)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CheckMapLoop(float localy)
    {
        if ((int)(localy / 1000) > maploopcnt)
        {
            int loop = (int)(localy / 1000) - maploopcnt;

            for (int i = 0; i < loop; i++)
            {
                maplist[maploopcnt % maplist.Count].transform.localPosition
                       += new Vector3(0, mapoffset * maplist.Count, 0);
                int percent = blockper - (maploopcnt / 3) <= 15 ? 15 : blockper - (maploopcnt / 3);

                maplist[maploopcnt % maplist.Count].SetData(percent);
                maploopcnt++;
            }
        }
    }

    public void UpdateUI()
    {
        lb_score.text = string.Format("{0:0.0}",score);
    }

    public void OnClick_Pause()
    {
        Time.timeScale = 0;
        GO_Pause.SetActive(true);

        GO_PauseBtn.SetActive(false);
        GO_PauseBtn.SetActive(true);
    }

    public void OnClick_PauseClose()
    {
        Time.timeScale = 1;
        GO_Pause.SetActive(false);
    }

    public void OnClick_Exit()
    {
        Debug.Log("Click Exit");
        SceneManager.LoadScene("Start");
    }
}
