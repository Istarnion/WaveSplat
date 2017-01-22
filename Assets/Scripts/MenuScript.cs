using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    private AudioClip menuOne, menuTwo;
    private bool soundFlip;
    private Button startBtn;

    void Start()
    {
        menuOne = (AudioClip)Resources.Load("Audio/Menu/menu_01");
        menuTwo = (AudioClip)Resources.Load("Audio/Menu/menu_02");

        startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        startBtn.Select();
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
        if (soundFlip) AudioSource.PlayClipAtPoint(menuOne, Camera.main.transform.position, .3f);
        else AudioSource.PlayClipAtPoint(menuTwo, Camera.main.transform.position, .3f);

        soundFlip = !soundFlip;
    }
}
