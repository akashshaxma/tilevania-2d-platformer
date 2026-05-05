using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    [SerializeField] string uniqueID;

    void Start()
    {
        if (string.IsNullOrEmpty(uniqueID))
        {
            Debug.LogWarning(gameObject.name + " has no Unique ID assigned!");
            return;
        }

        if (ScenePersist.instance != null && ScenePersist.instance.IsDestroyed(uniqueID))
        {
            Destroy(gameObject);
        }
    }

    public void DestroyPersistentObject()
    {
        if (ScenePersist.instance != null && !string.IsNullOrEmpty(uniqueID))
        {
            ScenePersist.instance.MarkDestroyed(uniqueID);
        }

        Destroy(gameObject);
    }
}