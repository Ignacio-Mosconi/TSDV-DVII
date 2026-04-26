using UnityEngine;

public class LevelLoadTest : MonoBehaviour
{
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LevelManager.Instance.LoadLevel(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            LevelManager.Instance.LoadLevel(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            LevelManager.Instance.LoadLevel(100);
    }
}