using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int playerHealth = 3;

    public Text scoreText;
    public Text healthText;

    public GameObject gameOverScreen;

    //Audio
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [ContextMenu("IncreaseScore")]
    public void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();

        audioManager.PlaySFX(audioManager.coin);

    }

    [ContextMenu("LOSEHealth")]
    public void loseHealth()
    {
        playerHealth--;
        healthText.text = playerHealth.ToString();

        if (playerHealth <= 0)
        {
            gameOver();
            audioManager.PlaySFX(audioManager.death);
        } else
        {
            audioManager.PlaySFX(audioManager.damaged);
        }

    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
