using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Singleton : MonoBehaviour {

    private static Player_Singleton playerSingleton;
    private int lives;
    private int points;
    private int previousScore;
    private float loadTime;
    private string nextLevel;

    public static Player_Singleton Instance
    {
        get { return playerSingleton; }
    }

    void Awake()
    {
        // if the singleton is null, it means it's the first time we created this class
        if (playerSingleton == null)
        {
            playerSingleton = this;
            lives = 3;
            points = 0;
            loadTime = 1.3f;
            nextLevel = "Credits";
            // the whole point of making a singleton is to create something that can't be destroyed
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // otherwise, we've already set it, so destroy this object
            Destroy(gameObject);
        }
    }

    public void WasHit()
    {
        lives--;

        if (lives < 0)
        {
            lives = 0;
        }

        if (lives != 0)
        {
            StartCoroutine("ReloadScene");
        } 
    }

    public void NextLevel(string levelName)
    {
        addPoints(lives * 2000);
        StartCoroutine("LoadNextScene", levelName);
    }

    public int getLives()
    {
        return lives;
    }

    public int addPoints(int p)
    {
        points += p;
        return points;
    }

    public int getPoints()
    {
        return points;
    }

    public void SetPreviousScore(int score)
    {
        previousScore = score;
    }
    public int GetPreviousScore()
    {
        return previousScore;
    }

    public void SaveAndLoad()
    {
        Score_Model scoreModel = GameObject.Find("I/O_Handler").GetComponent<Score_Model>();
        scoreModel.LoadHighScore();
        scoreModel.SaveScore();
    }

    public IEnumerator ReloadScene()
    {
        float endTime = Time.time + loadTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync("Level 1");
    }

    public IEnumerator LoadStartScene()
    {
        float endTime = Time.time + loadTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        playerSingleton = null;
        SceneManager.LoadSceneAsync("Start_Screen");
        Destroy(gameObject);
    }

    public IEnumerator LoadNextScene(string levelName)
    {
        float endTime = Time.time + loadTime;

        while (Time.time < endTime)
        {
            yield return null;
        }

        SceneManager.LoadSceneAsync(levelName);
        
    }
}
