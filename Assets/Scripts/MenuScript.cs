using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	void Start ()
    {
        Button btn = GameObject.Find("StartButton").GetComponent<Button>();
        btn.Select();
	}

    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
