using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public struct BackgroundElements
{
    //public Image Background;
    public string GameOverMessage;
    public Color color;
    public Sprite sprite;
    //public Texture texture;
};

public enum GameOverState { none, win, lose }

public class GameOverManager : MenuClass
{
    public static GameOverManager Instance { get; private set; }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("Scene Management")]
    [Tooltip("The index of the next Scene")]
    [SerializeField] private int nextLevelIndex;

    [Header("UI")]
    [SerializeField] private Image Background;
    [SerializeField] private TMP_Text gameOverMessage;
    [SerializeField] private Button NextLevelButton;

    [Header("Conditions")]
    [SerializeField] private BackgroundElements winBackgrounds;
    [SerializeField] private BackgroundElements loseBackground;

    public GameOverState currentGameOverState { get; private set; } = GameOverState.none;

    private void Awake()
    {
        InitializeSingleton();
        Background.gameObject.SetActive(false);
        NextLevelButton.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void OnGameOver(GameOverState gameOverState)
    {
        if (currentGameOverState != GameOverState.none) return;

        currentGameOverState = gameOverState;
        switch (currentGameOverState)
        {
            case GameOverState.win:
                Win();
                break;
            case GameOverState.lose:
                Lose();
                break;
            case GameOverState.none:
                break;
        }
    }

    private void Win()
    {
        ChangeBG(winBackgrounds);
        NextLevelButton.gameObject.SetActive(true);
    }

    private void Lose()
    {
        ChangeBG(loseBackground);
    }

    private void ChangeBG(BackgroundElements bgE)
    {
        Background.gameObject.SetActive(true);
        gameOverMessage.text = bgE.GameOverMessage;
        Background.color = bgE.color;
        if(bgE.sprite != null)
            Background.sprite = bgE.sprite;
    }

    public void LoadNextLevel()
    {
        LoadScene(nextLevelIndex);
    }

    public void WatchAd()
    {
        RewardedAdSample.Singleton.ShowAd();
    }

    public void RevivePlayer()
    {
        Debug.Log("reviving");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
