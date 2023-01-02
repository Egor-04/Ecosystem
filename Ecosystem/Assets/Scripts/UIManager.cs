using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CameraControl _cameraControl;

    private void Awake()
    {
        _cameraControl = FindObjectOfType<CameraControl>();
    }

    public void SetCameraState(bool lockState)
    {
        _cameraControl.IsLock = lockState;
    }

    public void OpenPanel(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
