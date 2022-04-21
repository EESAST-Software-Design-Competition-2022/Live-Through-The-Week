using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWelcome : MonoBehaviour
{
    private AsyncOperation asy;
    public GameObject audioPlayer;
    public AudioClip audioClip;
    private bool isSwitching = false;
    private float startTime = -1f;
    public Canvas canvas;
    private GameObject word;
    private GameObject about;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("name", 0);
        word = canvas.transform.Find("StartIntroduction").gameObject;
        about = canvas.transform.Find("About").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if(audioPlayer.GetComponent<AudioSource>().isPlaying == false && isSwitching)
        {
            asy.allowSceneActivation = true;
        }
        if(startTime > 0 && (Time.time - startTime) < 2)
        {
            word.GetComponent<CanvasGroup>().alpha = (Time.time - startTime)/2;
        }
    }
    public void startScene()
    {
        Destroy(this.gameObject.GetComponent<AudioSource>());
        asy = SceneManager.LoadSceneAsync("Floor1");
        asy.allowSceneActivation = false;
        isSwitching = true;
        audioPlayer.GetComponent<AudioSource>().PlayOneShot(audioClip);
        word.transform.SetAsLastSibling();
        startTime = Time.time;

    }

    public void showAbout()
    {
        about.GetComponent<CanvasGroup>().alpha = 1;
        about.transform.SetAsLastSibling();
    }

    public void dishowAbout()
    {
        about.GetComponent<CanvasGroup>().alpha = 0;
        about.transform.SetAsFirstSibling();
    }
}
