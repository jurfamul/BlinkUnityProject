using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Hazard : MonoBehaviour {

    // Hazard will be the parent class for all game objects that can kill the player, including obsticles, bullets, and 
    // enemies.
    public GameObject hazard;

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("spawn");
        GameObject tempHazard = Instantiate<GameObject>(hazard);
        tempHazard.transform.parent = GameObject.Find("Main Camera").transform;
    }
}
