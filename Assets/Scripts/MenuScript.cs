using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    private AudioClip menuOne, menuTwo;
    private bool soundFlip;

    void Start()
    {
        Button startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        startBtn.Select();

        Button quitBtn = GameObject.Find("QuitButton").GetComponent<Button>();

        menuOne = (AudioClip) Resources.Load("Audio/menu_01");
        menuTwo = (AudioClip) Resources.Load("Audio/menu_02");
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
        if (soundFlip) AudioSource.PlayClipAtPoint(menuOne, Camera.main.transform.position);
        else AudioSource.PlayClipAtPoint(menuTwo, Camera.main.transform.position);

        soundFlip = !soundFlip;
    }
}
