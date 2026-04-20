using UnityEngine;

public class LevelLoaderTest : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LevelsManager.Instance.LoadNewLevel(levelToLoad);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            LevelsManager.Instance.LoadNewLevelSegment(levelToLoad);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            LevelsManager.Instance.LoadNewLevelAsync(levelToLoad);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            LevelsManager.Instance.LoadNewLevelSegmentAsync(levelToLoad);
    }
}