using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    [SerializeField] private bool isPersistent = true;

    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
                instance = FindAnyObjectByType<T>();

            if (!instance)
                instance = new GameObject(nameof(T)).AddComponent<T>();

            return instance;
        }
    }


    void Awake ()
    {
        if (Instance == this)
        {
            if (isPersistent)
                DontDestroyOnLoad(gameObject);

            OnAwaken();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    protected virtual void OnAwaken () {}
}
