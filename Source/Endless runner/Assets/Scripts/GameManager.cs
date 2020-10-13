using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ResultObj;
    public CharacterMovement CM;
    public RoadSpawner RS;

    public Text PointsTxt;
    public Text LifesTxt;
    public Text HightScoreTxt;

    string currentDisance = "Current distance: ";
    string currentLifes = "Lifes: ";
    string hightScore = "Best distance: ";

    float Points = 0;
    public float HightScore;
    public float CurrentHightScore;

    public bool CanPlay;

    public void StartGame()
    {
        CanPlay = true;

        Points = CM.transform.position.x - RS.startPlayerPos.x;
        ResultObj.SetActive(false);

        CM.TurnOnRunAnimation();
        RS.StartGame();

        CM.LifesCount = 5;
        CM.CanPlay = true;

    }

    private void Update()
    {
        if (!CanPlay)
        {
            return;
        }
        PointsTxt.text = currentDisance + ((int)(CM.transform.position.x - RS.startPlayerPos.x - Points)).ToString();
        LifesTxt.text = currentLifes + CM.LifesCount.ToString();
    }

    public void ShowResult()
    {
        CurrentHightScore = CM.transform.position.x - RS.startPlayerPos.x - Points;
        SaveLoadManager.Instance.SaveGame();
        SaveLoadManager.Instance.LoadGame();
        ResultObj.SetActive(true);
    }

    public void RefreshText()
    {
        HightScoreTxt.text = hightScore +  HightScore.ToString();
    }
}
