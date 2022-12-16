using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void ReloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
