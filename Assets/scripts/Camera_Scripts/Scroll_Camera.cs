using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll_Camera : MonoBehaviour {

    Player_Singleton pSingleton;
    Vector3 startingPoint;
    public float scrollSpeed;
    public Text[] texts;
    public float timeToNextLevel;
    bool isGameOver;
    bool isComplete;
    bool hasShownScore;
    bool isLoading;
    Collider2D topCollider;

	// Use this for initialization
	void Start () {
        pSingleton = GameObject.Find("Game_Model").GetComponent<Player_Singleton>();
        startingPoint = new Vector3(0, 0, -10);
        transform.position = startingPoint;
        topCollider = transform.Find("Camera_Bounds_Top").GetComponent<Collider2D>();
        gameObject.GetComponent<AudioSource>().mute = false;
        isGameOver = false;
        isComplete = false;
        isLoading = false;
        hasShownScore = false;
        texts[0].text = "";
        texts[1].text = "";
        StartCoroutine("NullCheck");
	}
	
	// Update is called once per frame
	void Update () {

        if (!isGameOver && pSingleton.getLives() == 0)
        {
            texts[0].color = Color.red;
            texts[0].text = "Game Over";
            pSingleton.SaveAndLoad();
            isGameOver = true;
            StartCoroutine("ShowScore");

        }
        else if (transform.position.y <= 40f && !isGameOver)
        {
            float scrollStep = scrollSpeed * Time.deltaTime;
            transform.Translate(transform.up * scrollStep);
        }
        else if (transform.position.y > 40f && !isGameOver && !isComplete)
        {
            isComplete = true;
            texts[0].color = Color.blue;
            texts[0].text = "Level Complete";
            pSingleton.addPoints(10000);
            pSingleton.SaveAndLoad();
            StartCoroutine("ShowScore");
        }
        if (isGameOver && hasShownScore && !isLoading)
        {
            isLoading = true;
            pSingleton.StartCoroutine("LoadStartScene");
        }
        if (isComplete && hasShownScore && !isLoading)
        {
            isLoading = true;
            pSingleton.NextLevel("Credits");
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

    public IEnumerator ShowScore()
    {
        float endTime = Time.time + 5.0f;

        while (Time.time < endTime)
        {
            yield return null;
        }

        texts[0].text = "";
        texts[1].text = "New Score: " + pSingleton.getPoints() + "\nPrevious Score: " + pSingleton.GetPreviousScore();

        endTime = Time.time + 5.0f;

        while (Time.time < endTime)
        {
            yield return null;
        }

        hasShownScore = true;
        gameObject.GetComponent<AudioSource>().Stop();
    }

    public IEnumerator NullCheck()
    {
        while (true)
        {
            if (pSingleton == null)
            {
                pSingleton = GameObject.Find("Game_Model").GetComponent<Player_Singleton>();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}