using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    [SerializeField] Text scoreText;


    public static float bonus;
    public static bool gameEnd;
    public static float totalScore;
    [SerializeField] Text textPrefab;
    [SerializeField] InputField nameField;

//    static Text textPrefab;

    static scoreManager mainInst;

    // Use this for initialization
    void Start()
    {
        bonus = -transform.position.y;
        gameEnd = false;

        mainInst = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd)
        {
            return;
        }

        totalScore = (transform.position.y + bonus);
        scoreText.text = totalScore.ToString("0.0");
    }

    void InternalEndGame()
    {
        gameEnd = true;
        var gameEndObj = GameObject.Find("Canvas").transform.Find("GameEnd").gameObject;
        Debug.Log(gameEndObj);
        gameEndObj.SetActive(true);
        var personalHighScoreContent = gameEndObj.transform.Find("PersonalHighscore/Viewport/Content");
        List<float> storedHighscores = PlayerPrefs.GetString("personalHighscores", "0").Split(';').Select(float.Parse).ToList();

        storedHighscores.Add(totalScore);
        storedHighscores.Sort();
        storedHighscores.Reverse();

        foreach (float score in storedHighscores)
        {
            var txtInst = Instantiate(textPrefab);
            txtInst.transform.SetParent(personalHighScoreContent);
            txtInst.transform.localScale = Vector3.one;
            txtInst.text = score.ToString("0.0");
            if (totalScore == score)
            {
                txtInst.color = Color.green;
            }
        }

        PlayerPrefs.SetString("personalHighscores", String.Join(";", storedHighscores.Select(c => c.ToString("0.0")).ToArray()));
        PlayerPrefs.Save();

        var dreamlo = GameObject.Find("dreamloPrefab").GetComponent<dreamloLeaderBoard>();
        dreamlo.LoadScores();
        StartCoroutine(GlobalHighscoreStuff(dreamlo, gameEndObj));
    }

    IEnumerator GlobalHighscoreStuff(dreamloLeaderBoard dreamlo, GameObject gameEndObj)
    {
        yield return new WaitForSeconds(1f);
        while (dreamlo.ToStringArray() == null)
        {
            yield return new WaitForSeconds(0.25f);
        }

        List<dreamloLeaderBoard.Score> globalScores = dreamlo.ToListHighToLow();
        var globalHighScoreContent = gameEndObj.transform.Find("GlobalScoreList/GlobalHighscore/Viewport/Content");
        foreach (Transform child in globalHighScoreContent.transform) {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < globalScores.Count; i++)
        {
            var score = globalScores[i];
            var txtInst = Instantiate(textPrefab);
            txtInst.transform.SetParent(globalHighScoreContent);
            txtInst.transform.localScale = Vector3.one;
            txtInst.text = (i + 1) + " - " + score.playerName + ": " + (score.score * 0.1f).ToString("0.0");
        }
    }

    public void SendScore()
    {
        var dreamlo = GameObject.Find("dreamloPrefab").GetComponent<dreamloLeaderBoard>();
        if (nameField.text.Length <= 0)
        {
            return;
        }
        dreamlo.AddScore(nameField.text, Mathf.RoundToInt(totalScore * 10));
        var gameEndObj = GameObject.Find("Canvas").transform.Find("GameEnd").gameObject;
        StartCoroutine(GlobalHighscoreStuff(dreamlo, gameEndObj));
    }

    public static void EndGame()
    {
        mainInst.InternalEndGame();
    }
}