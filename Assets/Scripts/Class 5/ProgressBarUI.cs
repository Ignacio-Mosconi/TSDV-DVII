using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressScreen;
    [SerializeField] private Image progressBar;


    void Awake()
    {
        progressScreen.SetActive(false);
    }

    void Start ()
    {
        LevelsManager.Instance.OnLevelLoadStarted += OnLevelLoadStarted;
        LevelsManager.Instance.OnLevelLoadProgressed += OnLevelLoadProgress;
        LevelsManager.Instance.OnLevelLoadCompleted += OnLevelLoadCompleted;
    }
    
    void OnDestroy ()
    {
        LevelsManager.Instance.OnLevelLoadStarted -= OnLevelLoadStarted;
        LevelsManager.Instance.OnLevelLoadProgressed -= OnLevelLoadProgress;
        LevelsManager.Instance.OnLevelLoadCompleted -= OnLevelLoadCompleted;
    }


    private void OnLevelLoadStarted (string levelName)
    {
        Debug.Log($"Started loading level {levelName}");
        progressScreen.SetActive(true);
    }

    private void OnLevelLoadProgress (string levelName, float progress)
    {
        Debug.Log($"Loading level {levelName}...");
        progressBar.fillAmount = progress;
    }

    private void OnLevelLoadCompleted (string levelName)
    {
        Debug.Log($"Loaded level {levelName}!");
        progressScreen.SetActive(false);
    }
}