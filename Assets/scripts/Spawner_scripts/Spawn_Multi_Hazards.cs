using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Multi_Hazards : MonoBehaviour {

    public int hazards;
    public GameObject hazard;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < hazards; i++)
        {
            print("spawn");
            GameObject tempHazard = Instantiate<GameObject>(hazard);
            tempHazard.transform.parent = GameObject.Find("Main Camera").transform;
        }
    }
}
