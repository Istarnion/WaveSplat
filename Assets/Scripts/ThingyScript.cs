using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingyScript : MonoBehaviour
{

    public bool good = true;
    bool hasBeenHit = false;

	void Start ()
    {
	}

    public void Hit()
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;


        }
    }
}
