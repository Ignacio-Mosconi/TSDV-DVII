using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuLayout;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    public static bool IsPaused { get; private set; } 


    void Start ()
    {
        resumeButton.onClick.AddListener(OnPressResume);
        exitButton.onClick.AddListener(OnPressExit);
        pauseMenuLayout.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }
    
    void OnDestroy ()
    {
        resumeButton.onClick.RemoveListener(OnPressResume);
        exitButton.onClick.RemoveListener(OnPressExit);
    }

    private void Pause ()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        pauseMenuLayout.SetActive(true);
    }
    
    private void Unpause ()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        pauseMenuLayout.SetActive(false);
    }

    private void OnPressResume ()
    {
        Unpause();
    }

    private void OnPressExit ()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}