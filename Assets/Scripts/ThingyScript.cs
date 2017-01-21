using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyScript : MonoBehaviour
{
    public bool good = true;
    bool hasBeenHit = false;

    GameManager manager;

	void Awake()
    {
        manager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
	}

    public void Hit()
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;

            if (good)
            {
                manager.GoodHit();
            }
            else
            {
                manager.BadHit();
            }
        }
    }
}
