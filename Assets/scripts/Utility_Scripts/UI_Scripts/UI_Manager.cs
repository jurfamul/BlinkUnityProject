using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public Canvas canvas;
    public Text[] labels;
    public InputField[] inputFields;

	public void ChangeTextColor(Color32 newColor, int index)
    {
        if (index < labels.Length)
        {
           labels[index].color = newColor;
        }
    }

    public void ChangemultipleTextColors(Color32 newColor, int[] indexes)
    {
        foreach (int index in indexes)
        {
            if (index < labels.Length)
            {
                labels[index].color = newColor;
            }
        }
    }

    public void ChangeTextMessage(string message, int index)
    {
        if (index < labels.Length)
        {
            labels[index].text = message;
        }
    }

    /*
     * Changes the text of the indexed text elements to the messages given in the string array. The length of the two input arrays must be the
     * same as the function will place each message in the matching index in the index array.
     * <example> if messages[0] = "Hello World", indexes[0] = 3, then Hello World will be writen to the text element at index 3.
     */
    public void ChangeMultipleTextMessages(string[] messages, int[] indexes)
    {
        if (messages.Length != indexes.Length)
        {
            Debug.Log("ChangeMultipleTextMessages Error: The number of messages does not match the number of indexes.");
        }
        else
        {
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] < labels.Length)
                {
                    labels[indexes[i]].text = messages[i];
                }
            }
        }
    }
}
