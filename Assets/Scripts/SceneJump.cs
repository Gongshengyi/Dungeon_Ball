using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJump : MonoBehaviour
{
    public Button myButton;
    void Start()
    {
        myButton.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene("DungeonStartScene");
        }
         );

    }
}
