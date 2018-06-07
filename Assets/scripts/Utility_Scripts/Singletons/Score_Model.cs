using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we need these namespaces for serialization
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class Score_Model : MonoBehaviour {

    private static Score_Model sSingleton;

    public static Score_Model Instance { get { return sSingleton; } }

    public GameObject gameModel;
    public string saveFileName;

    void Awake()
    {
        if (sSingleton == null)
        {
            sSingleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameModel()
    {
        gameModel = GameObject.Find("Game_Model");
    }

    public void SaveScore()
    {
        if (gameModel == null)
        {
            SetGameModel();
        }
        Save_Score newScore = new Save_Score();
        SaveScoreModel(newScore, saveFileName);
    }

    public void LoadHighScore()
    {
        if (gameModel == null)
        {
            SetGameModel();
        }
        LoadScoreModel(saveFileName);
    }

    public void SaveScoreModel(Save_Score score, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        // then create a file stream that can be opened or created, with write access to it
        FileStream fs = File.OpenWrite(Application.persistentDataPath + "/" + fileName + ".dat");

        // note that we store the data from our game model (this object)
        // first in the SaveGame instance, think of SaveGame like a file
        score.StoreScore(this);

        // then we can serialize it to the disk using Serialize and
        // we serialize the SaveGame object. 
        bf.Serialize(fs, score);

        // close the file stream
        fs.Close();
    }

    public void LoadScoreModel(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        try
        {
            FileStream fs = File.OpenRead(Application.persistentDataPath + "/" + filename + ".dat");

            // deserialize the save game--this will throw an exception if we can't
            // deserialize from the file stream

            Save_Score saveScore = (Save_Score)bf.Deserialize(fs);

            // we assume we have access to the game model
            saveScore.LoadScore(this);

            // close the file stream
            fs.Close();
        }
        catch (FileNotFoundException e)
        {
            gameModel.GetComponent<Player_Singleton>().SetPreviousScore(0);
        }
    }
}
