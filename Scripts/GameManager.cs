using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float Score; // Score, used to display it in text 

    public GameObject textHolder;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void Update()
    {
        if (scoreText == null)
        {
            textHolder = GameObject.Find("ScoreText");
            scoreText = textHolder.GetComponent<Text>();
        }
        setScoreText();

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void NormalMode()
    {
        SceneManager.LoadScene("NormalLevel");
    }

    public void EndScreen()
    {
        SceneManager.LoadScene("EndScreen");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("HardLevel");
    }

    public void BackToStart()
    {
        Score = 0;
        SceneManager.LoadScene("Start");
    }

    public void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    void setScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score" + Score.ToString();
        }
    }

}
