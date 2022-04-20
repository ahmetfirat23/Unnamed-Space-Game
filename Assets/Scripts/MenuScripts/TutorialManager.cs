using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextLevel()
    {
        SceneManager.LoadScene("Level1");
    }


}
