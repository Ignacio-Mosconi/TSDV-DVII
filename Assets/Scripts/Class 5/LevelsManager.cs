using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviourSingleton<LevelsManager>
{
    public event Action<string> OnLevelLoadStarted;
    public event Action<string, float> OnLevelLoadProgressed;
    public event Action<string> OnLevelLoadCompleted;


    private async void LoadLevelAsync (string levelName, bool additively)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName, (additively) ? LoadSceneMode.Single : 
                                                                                              LoadSceneMode.Additive);
        float lastPogress = 0f;

        OnLevelLoadStarted?.Invoke(levelName);

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress != lastPogress)
            {
                OnLevelLoadProgressed?.Invoke(levelName, asyncOperation.progress);
                lastPogress = asyncOperation.progress;
            }

            await Task.Yield();
        }

        OnLevelLoadCompleted?.Invoke(levelName);
    }

    public void LoadNewLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNewLevelSegment (string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
    }
    
    public async void LoadNewLevelAsync (string levelName)
    {
        LoadLevelAsync(levelName, additively: false);
    }

    public void LoadNewLevelSegmentAsync (string levelName)
    {
        LoadLevelAsync(levelName, additively: true);
    }
}