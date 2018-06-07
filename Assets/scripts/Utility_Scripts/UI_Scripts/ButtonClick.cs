using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public void OnClick(string levelName)
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(levelName);
    }
}
