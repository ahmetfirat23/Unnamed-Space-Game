using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
///
///</summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private AudioSource clickSound;

    public void PlayTutorial()
    {
        StartCoroutine(GameLoader("Tutorial"));
    }

    public void LoadPlayer()
    { 

        ///TODO Save system

    }

    IEnumerator GameLoader(string levelName)
    {
        anim.Play("transitionMain");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        StartCoroutine(Quit());
    }
    IEnumerator Quit()
    {
        anim.Play("transitionMain");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    private void Start()
    {
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
}
