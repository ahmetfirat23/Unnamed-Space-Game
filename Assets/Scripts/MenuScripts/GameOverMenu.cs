using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/// <summary>
///
///</summary>
public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource clickSound;
    [SerializeField]
    Animator anim;
    public TMP_Text highscore;
    public TMP_Text score;

    public void PlayAgain()
    {
        StartCoroutine(GameLoader(PlayerPrefs.GetString("lastLevel","Level1"))) ;
    }

    public void ReturnMenu()
    {
        StartCoroutine(GameLoader("MainMenu"));
    }

    public void QuitGame()
    {
        StartCoroutine(Quit());
    }

    public void Start()
    {
        highscore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        score.text = "Score: " + (PlayerPrefs.GetInt("Score", 0)).ToString();
        Button[] btn = FindObjectsOfType<Button>();
        foreach (Button b in btn)
        {
            b.onClick.AddListener(Onclick);
        }
    }
    void Onclick()
    {
        clickSound.Play();
    }
    IEnumerator GameLoader(string levelName)
    {
        anim.Play("transitionmain");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }
    IEnumerator Quit()
    {
        anim.Play("transitionmain");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

}