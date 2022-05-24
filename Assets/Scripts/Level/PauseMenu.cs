using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Attributes:")]
    public static bool gameIsPaused;

    [Space]
    [Header("References")]
    public GameObject pauseMenuUi;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        pauseMenuUi.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void ExitToMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}