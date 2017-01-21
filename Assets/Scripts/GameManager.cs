using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Object[] levels;
    public int currentLevelIndex = 0;
    private LevelScript currentLevel;

    public Image faderImg;
    public Text timerText;
    public SplatManager splatter;
    public PlayerMovement playerMovement;

    private bool updateTimer = false;

	void Start ()
    {
        levels = Resources.LoadAll("Levels", typeof(GameObject));

        currentLevel = ((GameObject)Instantiate(levels[0])).GetComponent<LevelScript>();

        StartCoroutine(GotoNextLevel());
	}

    void Update()
    {
        if (currentLevel)
        {
            if (currentLevel.timeLeft < 0)
            {
                currentLevel.timeLeft = currentLevel.timeLimit;
                StartCoroutine(GotoNextLevel());
            }
            else
            {
                if (updateTimer)
                {
                    timerText.text = string.Format("{0:00}", Mathf.Round(currentLevel.timeLeft));
                    currentLevel.timeLeft -= Time.deltaTime;
                }
            }
        }
    }

    public void GoodHit()
    {
        if (++currentLevel.thingiesHit >= currentLevel.numThingies)
        {
            // TODO: Play level complete sound

            if (++currentLevelIndex < levels.Length)
            {
                StartCoroutine(GotoNextLevel());
            }
            else
            {
                currentLevelIndex = levels.Length - 1;
                faderImg.CrossFadeAlpha(1, 1, false);
            }
        }
        else
        {
            // TODO: Play thingy get sound
        }
    }

    public void BadHit()
    {
        // TODO: Play bad sound, be bad.
        currentLevel.timeLeft -= 5;
    }

    IEnumerator GotoNextLevel()
    {
        updateTimer = false;
        const float fadeTime = 0.7f;
        playerMovement.inControll = false;
        faderImg.CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        Destroy(currentLevel.gameObject);
        splatter.ClearSplats();
        currentLevel = ((GameObject)Instantiate(levels[currentLevelIndex])).GetComponent<LevelScript>();
        playerMovement.transform.position = currentLevel.playerSpawnPoint;
        faderImg.CrossFadeAlpha(0, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);
        playerMovement.inControll = true;
        updateTimer = true;
    }
}
