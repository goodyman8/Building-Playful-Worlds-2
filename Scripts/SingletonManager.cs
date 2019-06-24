using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    //Singleton test
    public static SingletonManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject); //Destroy duplicate game objects with the singleton
        }
    }
}
