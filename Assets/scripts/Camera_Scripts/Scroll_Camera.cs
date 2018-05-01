using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_Camera : MonoBehaviour {

    Vector3 startingPoint;
    public float scrollSpeed;
    Collider2D topCollider;
    Collider2D backgroundCollider;

	// Use this for initialization
	void Start () {
        startingPoint = transform.position;
        topCollider = transform.Find("Camera_Bounds_Top").GetComponent<Collider2D>();
        
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= 40f && !transform.Find("Player_Ship").GetComponent<Animator>().GetBool("isDead"))
        {
            float scrollStep = scrollSpeed * Time.deltaTime;
            transform.Translate(transform.up * scrollStep);
        }
        else if (transform.position.y > 40f && !transform.Find("Player_Ship").GetComponent<Animator>().GetBool("isDead"))
        {
            print("Level Complete");
        }
        else if (transform.Find("Player_Ship").GetComponent<Animator>().GetBool("isDead"))
        {
            print("Game Over");
        }
	}
}
