//using UnityEngine;

//public class ScenePersist : MonoBehaviour
//{
//    void Awake()
//    {
//        int numberScenePersists = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;

//        if (numberScenePersists > 1)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            DontDestroyOnLoad(gameObject);
//        }
//    }
//    public void ResetScenePersist()
//    {
//        Destroy(gameObject);
//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist instance;

    private HashSet<string> destroyedObjects = new HashSet<string>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void MarkDestroyed(string id)
    {
        destroyedObjects.Add(id);
    }

    public bool IsDestroyed(string id)
    {
        return destroyedObjects.Contains(id);
    }

    public void ResetPersistence()
    {
        destroyedObjects.Clear();
    }
}