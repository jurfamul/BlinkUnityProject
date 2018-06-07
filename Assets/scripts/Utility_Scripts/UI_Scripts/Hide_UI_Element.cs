using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hide_UI_Element : MonoBehaviour {

    // Idea for this class was provided by unity forum user: DiegoSLTS
    CanvasGroup group;

    private void Start()
    {
        group = gameObject.GetComponent<CanvasGroup>();
    }

    public void Hide()
    {
        group.alpha = 0f;
        group.blocksRaycasts = false;
    }

    public void Show()
    {
        group.alpha = 1.0f;
        group.blocksRaycasts = true;
    }
}
