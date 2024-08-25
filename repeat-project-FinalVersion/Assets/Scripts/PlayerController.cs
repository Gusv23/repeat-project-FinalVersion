using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10.0f;
    public float jumpForce = 10.0f;

    public AudioClip jumpSound;
    public AudioClip collectSound;

    private AudioSource playerAudio;

    private int collectibleCount = 0;
    private int requiredCollectibles = 3; // Number of collectibles needed to activate the portal
    public int lives = 2;  // Start with 2 lives
    private DisplayManager displayManager;

    // Boundary values based on the plane's position and scale
    public Vector3 boundaryMin = new Vector3(-10f, 0, 0f);
    public Vector3 boundaryMax = new Vector3(94, 0, 0f);

    void Start()
    {
        // Get and set up the Rigidbody
        playerRb = GetComponent<Rigidbody>();
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Initialize the DisplayManager
        displayManager = FindObjectOfType<DisplayManager>();
        if (displayManager != null)
        {
            displayManager.InitializeDisplay(lives, collectibleCount); // Passing only lives and collectibles
        }
        

        // Get the AudioSource component
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Handle player movement (horizontal and jumping)
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(playerRb.velocity.y) < 0.01f)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            // Play jump sound if assigned
            if (jumpSound != null)
            {
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
        }

        // Check for boundries (X and Z only) - Always there
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boundaryMin.x, boundaryMax.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, boundaryMin.z, boundaryMax.z);

        transform.position = clampedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the player collides with a collectible
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            collectibleCount++;
            displayManager.UpdateCollectibles(1);

            // Play collect sound if set
            if (collectSound != null)
            {
                playerAudio.PlayOneShot(collectSound, 1.0f);
            }

            Debug.Log("Collectibles: " + collectibleCount);
        }

        // Checks if player collides with a portal
        if (other.CompareTag("Portal"))
        {
            if (collectibleCount >= requiredCollectibles)
            {
                Debug.Log("Portal activated! Loading Level 2...");
                SceneManager.LoadScene("Level 2");
            }
            else
            {
                Debug.Log("Not enough collectibles! You need " + requiredCollectibles + " to pass.");
            }
        }

        // Checks if player collides with an obstacle
        if (other.CompareTag("Obstacle"))
        {
            LoseLife();
        }
    }
        // Decrease the number of lives and check if the player has any left, if not loads the "Looser" scene
    public void LoseLife()
    {
        lives--;
        displayManager.UpdateLives(-1);
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Looser");
        }
    }
}
