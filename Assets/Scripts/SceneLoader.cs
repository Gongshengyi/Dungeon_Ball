using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理的命名空间


public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Quitgame()
    {
        Debug.Log("QuitPress");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
    Application.Quit();

#endif
    }
}