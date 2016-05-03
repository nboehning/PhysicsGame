using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void _BtnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void _BtnPause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void _BtnResume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void _BtnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void _BtnQuit()
    {
        Application.Quit();
    }
}
