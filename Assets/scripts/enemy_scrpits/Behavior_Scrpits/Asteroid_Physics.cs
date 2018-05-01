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
        spawnPoint.x += Random.Range(-11, 11);
        spawnPoint.y += 5f;
        spawnPoint.z += 1;
        transform.position = spawnPoint;
        float forceX;
        if (Random.Range(0, 2) == 0)
        {
            forceX = 50f;
        }
        else
        {
            forceX = -50f;
        }
        if (Random.Range(0, 4) < 1)
        {
            if (Random.Range(0, 2) == 0)
            {
                forceX = Random.Range(-200, -100);
            }
            else
            {
                forceX = Random.Range(100, 200);
            }
        }
        float forceY = -50;
        Vector3 startingForce = new Vector3(forceX, forceY, 0);
        asteriod.AddForce(startingForce);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Camera_Bound_Top").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Camera_Bound_Bottom").GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        asteriod.velocity *= 0.9f;        
    }
}
