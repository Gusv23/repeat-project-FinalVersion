using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayManager : MonoBehaviour
{
    public Text timeText;
    public Text livesText;
    public Text collectiblesText;

    public float initialTime = 60f; // Editable in the Inspector
    private float timeRemaining;
    private int lives;
    private int collectibles;

    void Start()
    {
        // Initialize the timer with the value set in the Inspector
        timeRemaining = initialTime;

        // Initialize the display with current lives and collectibles
        UpdateTimeDisplay();
        UpdateLivesDisplay();
        UpdateCollectiblesDisplay();
    }

    void Update()
    {
        // Countdown the timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeDisplay();

            if (timeRemaining <= 0)
            {
                Debug.Log("Time's up!");
                LoadLooserScene();
            }
        }
    }

    public void InitializeDisplay(int initialLives, int initialCollectibles)
    {
        // Set up initial values for the game
        lives = initialLives;
        collectibles = initialCollectibles;

        // Update the display
        UpdateLivesDisplay();
        UpdateCollectiblesDisplay();
    }

    public void UpdateLives(int change)
    {
        lives += change;
        UpdateLivesDisplay();
    }

    public void UpdateCollectibles(int change)
    {
        collectibles += change;
        UpdateCollectiblesDisplay();
    }

    private void UpdateTimeDisplay()
    {
        timeText.text = "Time: " + Mathf.Max(0, Mathf.Round(timeRemaining)).ToString();
    }

    private void UpdateLivesDisplay()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void UpdateCollectiblesDisplay()
    {
        collectiblesText.text = "Collectibles: " + collectibles.ToString();
    }

    private void LoadLooserScene()
    {
        SceneManager.LoadScene("Looser");
    }
}
