using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Singleton : MonoBehaviour {

    private static Player_Singleton playerSingleton;
    private int lives;
    private int points;
    private float loadTime;

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

        if (lives == 0)
        {
            StartCoroutine("LoadStartScene");
            Destroy(gameObject, 1.2f);
        }
        else
        {
            lives--;
            StartCoroutine("ReloadScene");
        } 
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

        SceneManager.LoadSceneAsync("Start_Screen");
        SceneManager.UnloadSceneAsync("Level 1");
    }
}
