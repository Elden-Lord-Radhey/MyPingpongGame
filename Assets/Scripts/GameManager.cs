using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1Goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI leaderboardText;

    private int Player1Score;
    private int Player2Score;
    private int winningScore = 10; // Set the score needed to win

    [Header("Leaderboard")]
    public LeaderboardManager leaderboardManager; // Reference to LeaderboardManager

    public void Player1Scored()
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();

        if (Player1Score >= winningScore || Player2Score >= winningScore)
        {
            EndGame();
        }
        else
        {
            ResetPosition();
        }
    }

    public void Player2Scored()
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();

        if (Player1Score >= winningScore || Player2Score >= winningScore)
        {
            EndGame();
        }
        else
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        player1Paddle.GetComponent<Paddle>().Reset();
        player2Paddle.GetComponent<Paddle>().Reset();
    }

    private void EndGame()
    {
        // Disable paddles and ball
        ball.SetActive(false);
        player1Paddle.SetActive(false);
        player2Paddle.SetActive(false);

        // Determine winner or draw
        string winner = "";
        if (Player1Score >= winningScore && Player2Score >= winningScore)
        {
            winner = "It's a Draw!";
        }
        else if (Player1Score >= winningScore)
        {
            winner = "Player 1 Wins!";
        }
        else if (Player2Score >= winningScore)
        {
            winner = "Player 2 Wins!";
        }

        // Add scores to leaderboard
        leaderboardManager.AddScore("Player 1", Player1Score);
        leaderboardManager.AddScore("Player 2", Player2Score);

        // Show the Game Over Panel
        gameOverPanel.SetActive(true);
        winnerText.text = winner;

        // Populate leaderboard in Game Over Panel
        UpdateLeaderboardText();
    }

    private void UpdateLeaderboardText()
    {
        leaderboardText.text = "Leaderboard\n";
        foreach (var playerScore in leaderboardManager.leaderboard.scores)
        {
            leaderboardText.text += $"{playerScore.playerName}: {playerScore.score}\n";
        }
    }
}
