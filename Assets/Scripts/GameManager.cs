using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager instance;

    // Game settings
    [SerializeField] private float _gameTime = 60f; // Tempo total de jogo em segundos
    [SerializeField] private float _spawnTime = 3f; // Tempo para criar inimigos
    [SerializeField] private float _playerHealth = 10.0f; // Quantidade inicial de vidas do Player

    // Game variables
    [SerializeField] private float _currentTime;
    [SerializeField] private float _playerCurrentHealth;
    [SerializeField] private bool _inGame;

    private void Awake()
    {
        // Singleton 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize game variables
        _currentTime = _gameTime;
        _playerCurrentHealth = _playerHealth;
        _inGame = false;
    }

    private void Update()
    {
        if (_inGame)
        {
            // Decrease game time
            _currentTime -= Time.deltaTime;

            // Check if game time has run out
            if (_currentTime <= 0f)
            {
                WinGame();
            }
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        // Decrease player Health and check if player is dead
        _playerCurrentHealth -= damage;
        if (_playerCurrentHealth <= 0.0f)
        {
            _playerCurrentHealth = 0.0f;
            EndGame();
        }
    }

    private void EndGame()
    {
        // End game code goes here
        // Debug.Log("Game over!");
        GameController.instance.currentState = GameController.CanvasState.InLose;
        GameController.instance.Lose();
        // Destroy any remaining enemies, reset game variables, show end screen, etc.
    }
    private void WinGame()
    {
        // End game code goes here
        // Debug.Log("Game over!");
        GameController.instance.currentState = GameController.CanvasState.InWin;
        GameController.instance.Win();
        // Destroy any remaining enemies, reset game variables, show end screen, etc.
    }
    public void ResetGame()
    {
        _currentTime = _gameTime;
        _playerCurrentHealth = _playerHealth;
        _inGame = false;
    }

    public float GameTime
    {
        get => _gameTime;
        set { _gameTime = value; }
    }
    public float SpawnTime
    {
        get => _spawnTime;
        set { _spawnTime = value; }
    }
    public bool InGame
    {
        get => _inGame;
        set { _inGame = value; }
    }
    public float PlayerHealth { get => _playerHealth; }
    public float PlayerCurrentHealth { get => _playerCurrentHealth; }
    public float CurrentTime { get => _currentTime; }
}
