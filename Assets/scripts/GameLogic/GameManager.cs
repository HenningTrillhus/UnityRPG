using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGamePaused = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
        PauseGame();
    }

    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // FREEZES physics, animations, Update() on non-Time-independent code
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
