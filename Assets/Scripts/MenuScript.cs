using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	void Start ()
    {
<<<<<<< HEAD
        Button startBtn = GameObject.Find("StartButton").GetComponent<Button>();
        startBtn.Select();

        Button quitBtn = GameObject.Find("QuitButton").GetComponent<Button>();

=======
        Button btn = GameObject.Find("StartButton").GetComponent<Button>();
        btn.Select();
>>>>>>> f8156d731fcc6fb9ba40d24b4b214a808dc978a4
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
