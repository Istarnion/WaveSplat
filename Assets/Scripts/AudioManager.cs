using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool splatSwitch;
    private AudioClip splatOne, splatTwo;
    private int hitIndex;
    private List<AudioClip> activeHitScale; // one array per instrument
    private List<AudioClip> activeLevelCompleteChords; // one per instr
    private List<AudioClip> backgroundMusic;

    void Awake()
    {
        splatOne = (AudioClip)Resources.Load("Audio/splat_01");
        splatTwo = (AudioClip)Resources.Load("Audio/splat_02");
        backgroundMusic = new List<AudioClip>();

        activeHitScale = new List<AudioClip>();
        activeLevelCompleteChords = new List<AudioClip>();

        LoadSounds("Audio/BackingLoops", backgroundMusic);
        LoadSounds("Audio/HitScale/Piano", activeHitScale);
        LoadSounds("Audio/LevelComplete/Piano", activeLevelCompleteChords);

        StartCoroutine(BackgroundMusic());
    }
    
    IEnumerator BackgroundMusic()
    {
        while(true)
        {
            int index = Random.Range(0, backgroundMusic.Count);
            AudioSource.PlayClipAtPoint(backgroundMusic[index], Camera.main.transform.position, 0.7f);
            yield return new WaitForSeconds(backgroundMusic[index].length - 0.499f); // magic
        }
    }

    void LoadSounds(string path, List<AudioClip> clips)
    {
        foreach (Object clip in Resources.LoadAll(path)) clips.Add((AudioClip)clip);
    }
    
    public void PlaySplat()
    {
        if (splatSwitch) AudioSource.PlayClipAtPoint(splatOne, Camera.main.transform.position, 0.3f);
        else AudioSource.PlayClipAtPoint(splatTwo, Camera.main.transform.position, 0.3f);
        splatSwitch = !splatSwitch;
    }

    public void PlayHit(int clipIndex)
    {
        clipIndex %= activeHitScale.Count;
        AudioSource.PlayClipAtPoint(activeHitScale[clipIndex], Camera.main.transform.position, 1f);
    }

    public void PlayLevelComplete(int clipIndex)
    {
        clipIndex %= activeLevelCompleteChords.Count;
        AudioSource.PlayClipAtPoint(activeLevelCompleteChords[clipIndex], Camera.main.transform.position, 1f);
    }
}
