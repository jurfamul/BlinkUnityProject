using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for serialization
using System;
using System.Runtime.Serialization;

[Serializable]
public class Save_Score : ISerializable
{
    public int Score { get; set; }

    public Save_Score()
    {

    }

    public Save_Score(SerializationInfo info,
        StreamingContext context)
    {
       Score = info.GetInt32("Score");
    }

    public void StoreScore(Score_Model model)
    {
        Score = model.gameModel.GetComponent<Player_Singleton>().getPoints();
    }

    public void LoadScore(Score_Model model)
    {
        model.gameModel.GetComponent<Player_Singleton>().SetPreviousScore(Score);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Score", Score);
    }
}
