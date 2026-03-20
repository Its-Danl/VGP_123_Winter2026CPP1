using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button resumeGame;

    [Header("In Game UI")]
    [SerializeField] private TMP_Text livesText;

    [Header("Menu References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(() => ChangeScene("Game"));
        if (creditsButton != null)
            creditsButton.onClick.AddListener(() => SetMenus(creditsMenu, mainMenu));
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
        if (backButton != null)
            backButton.onClick.AddListener(() => SetMenus(mainMenu, creditsMenu));
        if (returnToMenuButton != null)
            returnToMenuButton.onClick.AddListener(() => ChangeScene("Title"));
        if (resumeGame != null)
            resumeGame.onClick.AddListener(() =>
            {
                ToggleIsPaused(false);
                SetMenus(null, pauseMenu);
            });

        if (GameManager.Instance != null)
        {
            if (livesText != null)
                livesText.text = "Lives: " + GameManager.Instance.Lives;

            GameManager.Instance.OnLifeValueChanged += OnLivesChanged;
        }
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLifeValueChanged -= OnLivesChanged;
    }

    void OnLivesChanged(int lives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            ToggleIsPaused(true);
            SetMenus(deathMenu, null);
        }
    }

    void Update()
    {
        // If dead, only allow Escape to return to menu
        if (deathMenu != null && deathMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangeScene("Title");
            return;
        }

        // Handle pause toggle
        if (pauseMenu != null && Input.GetKeyDown(KeyCode.P))
        {
            bool isPausing = !pauseMenu.activeSelf;
            ToggleIsPaused(isPausing);
            SetMenus(isPausing ? pauseMenu : null, isPausing ? null : pauseMenu);
        }
    }

    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate != null)
            menuToActivate.SetActive(true);
        if (menuToDeactivate != null)
            menuToDeactivate.SetActive(false);
    }

    void ToggleIsPaused(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }

    void ChangeScene(string sceneName)
    {
        ToggleIsPaused(false);
        SceneManager.LoadScene(sceneName);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}