using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    #endregion

    #region Game Panels
    [Header("Panels")]
    public GameObject startPanel;
    public GameObject gameInPanel;
    public GameObject winPanel;
    public GameObject failPanel;
    #endregion

    #region Private-Public Variables
    public bool isStarted = false;
    #endregion

    #region Monobehaviour
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Set Game
    public void StartGame()
    {
        isStarted = true;

        startPanel.SetActive(false);
        gameInPanel.SetActive(true);
    }

    public void WinGame()
    {
        gameInPanel.SetActive(false);
        winPanel.SetActive(true);
        isStarted = false;
    }

    public void FailGame()
    {
        gameInPanel.SetActive(false);
        failPanel.SetActive(true);
        isStarted = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
