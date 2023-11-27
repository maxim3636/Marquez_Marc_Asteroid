using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3;
    private int score = 0;
    public TMP_Text lbl_Score;
    public TMP_Text lbl_BestScore;
    public TMP_Text lbl_Wave;
    private int wave = 1;
    public GameObject[] livesImages;
    public TMP_Text lbl_Restart;
    public TMP_Text lbl_Menu;
    public TMP_Text lbl_Win;
    public TMP_Text lbl_Lose;
    public GameObject Player;
    public GameObject Asteroids;
    public bool finish;
    private int initialBestScore;
    public AudioSource audioSource1;
    public AudioClip explosionSound;
    
    // Start is called before the first frame update
    void Start()
    {
        lbl_Score.text = "Score: " + 0;
        initialBestScore = PlayerPrefs.GetInt("BestScore");
        InvokeRepeating("Increasewave", 10f, 10f);
    }
    void Increasewave()
    {
        wave++;
    }

    // Update is called once per frame
    void Update()
    {
        lbl_Wave.text = "Wave: " + wave;
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        lbl_BestScore.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");
        if (finish)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    public void AddScore()
    {
        audioSource1.clip = explosionSound;
        audioSource1.Play();
        score++;
        lbl_Score.text = "Score: " + score;
    }

    public void LiveControl()
    {
        lives--;
        if (lives == 2)
        {
            livesImages[0].SetActive(false);
        }
        else if (lives == 1)
        {
            livesImages[1].SetActive(false);
        }
        else if (lives == 0)
        {
            Player.SetActive(false);
            Asteroids.SetActive(false);
            livesImages[2].SetActive(false);
            lbl_Menu.gameObject.SetActive(true);
            lbl_Restart.gameObject.SetActive(true);
            check();
            finish = true;
        }
    }

    public void check()
    {
        if (initialBestScore >  score)
        {
            lbl_Lose.gameObject.SetActive(true);
        }
        else if (initialBestScore <  score)
        {
            lbl_Win.gameObject.SetActive(true);
        }
    }
}
