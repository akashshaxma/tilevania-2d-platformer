//using System.Collections;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class LevelExit : MonoBehaviour
//{
//    [SerializeField]  float levelLoadDelay = 1f;

//   void OnTriggerEnter2D(Collider2D other)
//    {
//        StartCoroutine(LoadNextLevel());     

//    }
//    IEnumerator LoadNextLevel()
//    {
//        yield return new WaitForSeconds(levelLoadDelay);
//        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

//        int nextSceneIndex = currentSceneIndex + 1;

//        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
//        {
//            nextSceneIndex = 0;
//        }
//        //FindFirstObjectByType<ScenePersist>().ResetScenePersist();
//        SceneManager.LoadScene(nextSceneIndex);
//    }

//}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.levelCompleteSFX);
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}