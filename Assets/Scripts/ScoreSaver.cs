using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    public GameManager gameManager;
    private Text t_Score;
    private Text t_User;
    private bool gotScore = false;
    //private Text t_User;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        t_Score = GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gotScore == false)
        {
            t_Score.text = "Ton score : " + gameManager.score;
            gotScore = true;
        }
        
    }

    //WIP
    public void GetName()
    {
        //Créer un tableau avec Rang, Score et nom (potentiellement en créant une classe)
        //Trier le tableau
        //L'enregistrer dans un objet JsonUtility (JsonUtility.ToJson)
        //Convertir le JsonUtility en string et l'enregistrer avec PlayerPrefs
        string s_User;
        t_User.text = GetComponent<InputField>().text;
        s_User = t_User.text;
        PlayerPrefs.SetString("Highscore", gameManager.score.ToString());
    }

    public void ShowScoreboard()
    {
        string jsonGet;
        jsonGet = PlayerPrefs.GetString("Highscore");
        //créer une liste avec JsonUtility.FromJson<ClasseName>
    }
}
