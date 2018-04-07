using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

    public static List<Score> score = new List<Score>();

    public static void AddScore(string player, int scoreToAdd)
    {
        var idx = score.FindIndex(s => s.player == player);
        score[idx].score += scoreToAdd;
    }

    [System.Serializable]
    public class Score
    {
        public string player = "";
        public int score = 0;
    }
    public void Start()
    {
        for (int i = 0; i < GameContext.Instance.ships.Count; i++)
        {
            Score x = new Score();
            x.player = GameContext.Instance.ships[i].ownerName;
            x.score = 0;
            score.Add(x);
        }
    }
    public void Update()
    {
        Debug.Log(GenerateLogString());
    }

    string GenerateLogString()
    {
        string result = "";
        for (int i = 0; i < score.Count; i++)
        {
            result += score[i].player + ":" + score[i].score + "///==///";
        }
        return result;
    }
}
