using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	void Start ()
    {
        Button startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        startBtn.Select();

        Button quitBtn = GameObject.Find("QuitButton").GetComponent<Button>();

	}

    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void PlayMenuSelectSound()
    {

    }
}
