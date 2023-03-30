using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public PlayerController playerController;

    [Header("Parameters")]
    public int totalPlanetHealth;
    public int totalEnemies;

    [Header("Bars")]
    public PipBar planetBar;
    public PipBar mineBar;
    public int mineThreshold;
    private int mineCharge = 0;

    [HideInInspector] public int enemiesLeftToSpawn;

    private int enemiesRemaining;
    public int EnemiesRemaining 
    { 
        get { return enemiesRemaining; }
        set
        {
            enemiesRemaining = value;
            if(enemiesRemaining < 0)
            {
                enemiesRemaining = 0;
            }
            enemiesRemainingText.text = $"Enemies Remaining: {enemiesRemaining}";
        }
    }

    private int currentPlanetHealth;
    public int CurrentPlanetHealth
    {
        get { return currentPlanetHealth; }
        set
        {
            currentPlanetHealth = value;
            if(currentPlanetHealth < 0)
            {
                currentPlanetHealth = 0;
            }
        }
    }

    [Header("UI")]
    public TMP_Text enemiesRemainingText;
    public GameObject mineHighlight;
    public TMP_Text mineStatus;
    public TMP_Text letterGrade;
    public GameObject alert;
    
    [Header("Pages")]
    public GameObject tutorialScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private bool gameOver = false;

    private void Awake()
    {
        Time.timeScale = 0f;

        Instance = this;

        enemiesRemainingText.text = $"Enemies Remaining: {totalEnemies}";

        EnemiesRemaining = totalEnemies;
        enemiesLeftToSpawn = totalEnemies;
        currentPlanetHealth = totalPlanetHealth;
    }

    private void Update()
    {
        if (currentPlanetHealth <= 0 && gameOver == false)
        {
            gameOver = true;
            Lose();
        }
        else if (EnemiesRemaining <= 0 && gameOver == false)
        {
            gameOver = true;
            Win();
        }
    }

    public void BeginGame()
    {
        Time.timeScale = 1f;
        tutorialScreen.SetActive(false);
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    private void Lose()
    {
        loseScreen.SetActive(true);
        StopGame();
    }

    private void Win()
    {
        switch (currentPlanetHealth)
        {
            case 1:
                letterGrade.text = "D";
                break;
            case 2:
                letterGrade.text = "C";
                break;
            case 3:
                letterGrade.text = "B";
                break;
            case 4:
                letterGrade.text = "A";
                break;
            case 5:
                letterGrade.text = "S";
                break;
            default:
                break;
        }

        winScreen.SetActive(true);

        SoundManager.Instance.PlayVictoryMusic();

        StopGame();
    }

    public void OnEnemyDeath(CauseOfDeath cause)
    {
        if(cause == CauseOfDeath.projectile)
        {
            mineCharge++;
            mineBar.RaisePips();
            if(mineCharge >= mineThreshold)
            {
                playerController.canMine = true;
                mineHighlight.SetActive(true);
                mineStatus.text = "Mine Ready";
            }
        }
        else if(cause == CauseOfDeath.planet)
        {
            StartCoroutine(Alert());
            SoundManager.Instance.PlayAlert();
            planetBar.LowerPips();
        }
    }

    public void OnMineUsed()
    {
        mineCharge = 0;
        mineHighlight.SetActive(false);
        mineStatus.text = "Mine Charging...";
        mineBar.EmptyBar();
    }

    private IEnumerator Alert()
    {
        alert.SetActive(true);

        yield return new WaitForSeconds(2f);

        alert.SetActive(false);
    }
}
