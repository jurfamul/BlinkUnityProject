using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Singleton : MonoBehaviour {

    private static Player_Singleton playerSingleton;
    public int lives;
    public int points;
    public float loadTime;

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
}
