using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    // public TextMeshProUGUI text;
    int score;
    public TextMeshProUGUI scoreuI;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Update()
    {
       /* if (score == 11)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }*/
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        scoreuI.text = "Jump(Mileage): " + score.ToString();

    }

}
