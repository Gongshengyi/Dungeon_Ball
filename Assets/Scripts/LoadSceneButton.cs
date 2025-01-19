using System.Collections;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LodaSceneButton : MonoBehaviour
{
    public Button myButton;
    void Start()
    {
        myButton.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("RunningScene");
        }
         );

        Time.timeScale = 1f;

    }

}
