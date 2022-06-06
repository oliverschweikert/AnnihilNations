using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Attributes:")]
    public static bool gameIsPaused;

    [Space]
    [Header("References")]
    public GameObject pauseMenuUi, gameUI;
    public Player player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!player.dead)
            {
                if (gameIsPaused) ResumeGame();
                else PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        pauseMenuUi.SetActive(true);
        gameUI.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        gameUI.SetActive(true);
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void ExitToMenu()
    {
        Cursor.visible = true;
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}