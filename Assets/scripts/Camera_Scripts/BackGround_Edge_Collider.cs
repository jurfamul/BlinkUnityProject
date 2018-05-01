using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Edge_Collider : MonoBehaviour {

    public static Vector3 Position;
    public static Collider2D backgroundEdgeCollider;
	// Use this for initialization
	void Start () {
        Position = transform.position;
        backgroundEdgeCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
