using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TextMeshProUGUI mainTitle;
    [SerializeField] private TextMeshProUGUI timeLeftValue;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private bool isPaused;

    public enum CanvasState
    {
        InPause,
        InWin,
        InLose,
        InGame
    }

    public CanvasState currentState;

    private void Awake()
    {
        instance = this;
        currentState = CanvasState.InGame;
        Resume();
        gamePanel.SetActive(false);
        restartButton.onClick.AddListener(OnClickRestartButton);
        menuButton.onClick.AddListener(OnClickMenuButton);
        closeButton.onClick.AddListener(Resume);
        pauseButton.onClick.AddListener(Pause);
    }

    private void Update()
    {
        float time = GameManager.instance.CurrentTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string formattedTime = timeSpan.ToString(@"mm\:ss\:ff");
        timeLeftValue.text = formattedTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {

                Pause();
            }
            else
            {

                Resume();
            }
        }
    }
    private void OnClickRestartButton()
    {
        SceneManager.LoadScene("Game");
        GameManager.instance.ResetGame();
        Resume();
    }
    private void OnClickMenuButton()
    {
        SceneManager.LoadScene("Menu");
        GameManager.instance.ResetGame();
        Resume();
    }
    private void OnClickPauseButton()
    {
        pauseButton.interactable = false;
        Pause();
    }
    private void OnClickCloseButton()
    {
        Resume();
        pauseButton.interactable = true;
    }
    public void Pause()
    {
        currentState = CanvasState.InPause;
        mainTitle.text = "Pause";
        closeButton.gameObject.SetActive(true);
        gamePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        GameManager.instance.InGame = false;
    }

    public void Resume()
    {
        currentState = CanvasState.InGame;
        gamePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        GameManager.instance.InGame = true;
    }
    public void Lose()
    {
        currentState = CanvasState.InLose;
        pauseButton.interactable = false;
        mainTitle.text = "You Lose :/";
        closeButton.gameObject.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.InGame = false;
    }
    public void Win()
    {
        currentState = CanvasState.InWin;
        pauseButton.interactable = false;
        mainTitle.text = "You Win!!!";
        closeButton.gameObject.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.InGame = false;
    }
}
