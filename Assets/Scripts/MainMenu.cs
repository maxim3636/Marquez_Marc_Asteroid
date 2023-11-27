using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject HighScore;

    private Text lbl_HighScore;
    // Start is called before the first frame update
    void Start()
    {
        lbl_HighScore = HighScore.GetComponent<Text>();
        lbl_HighScore.text = "BEST SCORE: " + PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
