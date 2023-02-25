using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject optionsPanel;

    [Space(20)]
    [SerializeField] private TMP_InputField matchTime;
    [SerializeField] private TextMeshProUGUI matchTimePlaceHolder;
    [SerializeField] private TMP_InputField spawnTime;
    [SerializeField] private TextMeshProUGUI spawnTimePlaceHolder;
    [SerializeField] private Button closeOptionsButton;
    [SerializeField] private Button saveOptionsButton;
    [SerializeField] private GameObject saveFeedBack;

    private void Awake()
    {
        optionsPanel.SetActive(false);
        playButton.onClick.AddListener(OnPlayClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        saveOptionsButton.onClick.AddListener(OnSaveOptionsClicked);
        closeOptionsButton.onClick.AddListener(OnCloseOptionsClicked);
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene("Game");
        GameManager.instance.InGame = true;
    }
    private void OnOptionsClicked()
    {
        optionsPanel.SetActive(true);
        matchTimePlaceHolder.text = GameManager.instance.GameTime.ToString();
        spawnTimePlaceHolder.text = GameManager.instance.SpawnTime.ToString();
    }
    private void OnSaveOptionsClicked()
    {
        float _matchTime = GameManager.instance.GameTime;
        float _spawnTime = GameManager.instance.SpawnTime;

        if (float.TryParse(matchTime.text, out _matchTime))
        {
            GameManager.instance.GameTime = _matchTime;
        }
        if (float.TryParse(spawnTime.text, out _spawnTime))
        {
            GameManager.instance.SpawnTime = _spawnTime;
        }
        GameManager.instance.ResetGame();
        StartCoroutine(ShowSave());
    }
    private void OnCloseOptionsClicked()
    {
        optionsPanel.SetActive(false);
    }
    IEnumerator ShowSave()
    {
        saveFeedBack.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        saveFeedBack.SetActive(false);
    }
}
