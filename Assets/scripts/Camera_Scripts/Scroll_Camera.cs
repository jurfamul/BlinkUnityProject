using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll_Camera : MonoBehaviour {

    Vector3 startingPoint;
    public float scrollSpeed;
    public Text text;
    public float timeToNextLevel;
    bool isGameOver;
    Collider2D topCollider;
    Collider2D backgroundCollider;

	// Use this for initialization
	void Start () {
        startingPoint = transform.position;
        topCollider = transform.Find("Camera_Bounds_Top").GetComponent<Collider2D>();
        isGameOver = false;
        text.text = "";
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= 40f && !isGameOver)
        {
            float scrollStep = scrollSpeed * Time.deltaTime;
            transform.Translate(transform.up * scrollStep);
        }
        else if (transform.position.y > 40f && !isGameOver)
        {
            text.color = Color.blue;
            text.text = "Level Complete";
            gameObject.GetComponent<AudioSource>().mute = true;
            StartCoroutine("LoadNextScene");
        }
        else if (!isGameOver && transform.Find("Player_Ship").GetComponent<Animator>().GetBool("isDead"))
        {
            text.color = Color.red;
            text.text = "Game Over";
            gameObject.GetComponent<AudioSource>().mute = true;
            isGameOver = true;
            StartCoroutine("LoadNextScene");
        }
	}

    public IEnumerator LoadNextScene()
    {
        float endTime = Time.time + timeToNextLevel;

        while (Time.time < endTime)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync("Start_Screen");
        SceneManager.UnloadSceneAsync("Level 1");
    }
}