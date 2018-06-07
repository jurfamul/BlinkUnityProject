using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Physics : MonoBehaviour {

    Vector3 spawnPoint;
    Rigidbody2D asteriod;

	// Use this for initialization
	void Start () {
        asteriod = GetComponent<Rigidbody2D>();
        spawnPoint = transform.parent.transform.position;
        spawnPoint.x += Random.Range(-5, 11);
        spawnPoint.y += 7f;
        spawnPoint.z += 1;
        transform.position = spawnPoint;
        float forceX;
        if (spawnPoint.x < 0)
        {
            forceX = 50f;
        }
        else
        {
            forceX = -50f;
        }
        if (Random.Range(0, 4) < 1)
        {
            if (spawnPoint.x < 0)
            {
                forceX = Random.Range(-200, -100);
            }
            else
            {
                forceX = Random.Range(100, 200);
            }
        }
        float forceY = -300;
        Vector3 startingForce = new Vector3(forceX, forceY, 0);
        asteriod.AddForce(startingForce);
    }
	
	public void DestroyObsticle()
    {
        Destroy(gameObject);
    }

}
