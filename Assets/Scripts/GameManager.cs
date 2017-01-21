using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Object[] levels;
    public int currentLevelIndex = 0;
    private LevelScript currentLevel;

    public FloatingTextScript floatingText;

    public Image faderImg;
    public Text timerText;
    public SplatManager splatter;
    public PlayerMovement playerMovement;

    private bool updateTimer = false;

    private AudioManager audioManager;

    void Start ()
    {
        levels = Resources.LoadAll("Levels", typeof(GameObject));
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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

    public void GoodHit(ThingyScript thingy)
    {
        if (++currentLevel.thingiesHit >= currentLevel.numThingies)
        {
            audioManager.PlayLevelComplete(currentLevel.numThingies - 1);
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
            audioManager.PlayHit(currentLevel.thingiesHit - 1);
            currentLevel.timeLeft += 3;
            SpawnFloatingText(3, thingy.transform.localPosition);
        }
    }

    public void BadHit(ThingyScript thingy)
    {
        // TODO: Play bad sound,
        currentLevel.timeLeft -= 5;
        SpawnFloatingText(-5, thingy.transform.localPosition);
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

    private void SpawnFloatingText(int number, Vector3 pos)
    {
        floatingText.transform.position = pos;
        floatingText.number = number;
        Instantiate<FloatingTextScript>(floatingText);
    }
}
