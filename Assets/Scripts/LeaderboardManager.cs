using System.Collections.Generic;
using System.IO;
using UnityEngine;



[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class Leaderboard
{
    public List<PlayerScore> scores = new List<PlayerScore>();
}

public class LeaderboardManager : MonoBehaviour
{
    private string filePath;
    public Leaderboard leaderboard = new Leaderboard();

    void Start()
    {
        // Define the file path for the JSON
        filePath = Application.persistentDataPath + "/leaderboard.json";

        // Load existing leaderboard data
        LoadLeaderboard();
    }

    public void AddScore(string playerName, int score)
    {
        // Create a new score entry
        PlayerScore newScore = new PlayerScore
        {
            playerName = playerName,
            score = score
        };

        // Add the score to the leaderboard
        leaderboard.scores.Add(newScore);

        // Sort the leaderboard by score in descending order
        leaderboard.scores.Sort((a, b) => b.score.CompareTo(a.score));

        // Save the updated leaderboard
        SaveLeaderboard();
    }

    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Leaderboard saved to " + filePath);
    }

    private void LoadLeaderboard()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            leaderboard = JsonUtility.FromJson<Leaderboard>(json);
            Debug.Log("Leaderboard loaded from " + filePath);
        }
        else
        {
            Debug.Log("No leaderboard file found. Creating a new one.");
        }
    }
}
