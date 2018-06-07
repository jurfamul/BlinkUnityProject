using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

    public Text text;
    public Scrollbar scrollBar;
    public float speed;
    bool isLoading;

    // Use this for initialization
    void Start()
    {
        scrollBar.value += 1f;
        isLoading = false;
        text.text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + "Blink\n\nGame Designer: Jurgen Famula\nProgrammer: Jurgen Famula" +
            "\nProgramming advising provided by Chris GauthierDickey"
            + "\nDynamicScrollView code by: RJ45 on Unity Answers/n/nVisual Assets:\n\nShmup Ships by: surt aka Carl Olsson\n"
            + "Source: https://opengameart.org/content/shmup-ships" + "\n\nExplosion Spites by: unknown artist\nSource: " +
            "http://hasgraphics.com/phaedy-explosion-generator/spritesheet11/" + "\n\nBullet Sprites by: Master484\n"
            + "Source: http://m484games.ucoz.com/" + "\n\nDynamic Space Background Trial Pack by: DinV\nSource: Unity Asset Store"
            + "\n\nAudio Assets:\n\nBackground Music:\nLevel Up Kevin MacLeod (incompetech.com)\n Licensed under Creative Commons: By Attribution 3.0"
            + "\nhttp://creativecommons.org/licenses/by/3.0/" + "\nVoltaic Kevin MacLeod(incompetech.com)\n Licensed under Creative Commons: By Attribution 3.0"
           + "\nhttp://creativecommons.org/licenses/by/3.0/" + "\nLong Time Coming Kevin MacLeod(incompetech.com)\n Licensed under Creative Commons: By Attribution 3.0"
           + "\nhttp://creativecommons.org/licenses/by/3.0/"  + "\n\nSound Effects:\nExplosion Sound Effects by: jalastram" + "\nSource:"
            + "https://opengameart.org/content/8-bit-explosions-1" + "/nLaser Sound Effects by: dklon/nSource: https://opengameart.org/content/laser-fire"
            + "\n\n\nThank You for Playing!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
    }

    void Update()
    {
        scrollBar.value -= (Time.deltaTime * speed);
        if (scrollBar.value <= 0f && !isLoading)
        {
            isLoading = true;
            StartCoroutine("LoadMenu");
        }
    }

    public IEnumerator LoadMenu()
    {
        float endTime = Time.time + 3.0f;

        while (Time.time < endTime)
        {
            yield return null;
        }

        GameObject.Find("Camera").GetComponent<AudioSource>().Stop();
        SceneManager.LoadSceneAsync("Start_Screen");
    }
}
