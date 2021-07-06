using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    public void PauseGame() {
        if (!GameIsPaused) {
            Pause();
        }
    }

    public void ResumeGame() {
        if (GameIsPaused) {
            Resume();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    private void Resume () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


}
