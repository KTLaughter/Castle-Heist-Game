using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TMP_Text healthText; // Assign this in the Inspector

    public GameBehavior gameBehavior; // Reference to the GameBehavior component

    void Start()
    {
        // Get the GameBehavior component from the GameObject
        gameBehavior = FindObjectOfType<GameBehavior>();
    }



    void Update()
    {
        if (gameBehavior == null || healthText == null)
        {
            // Exit the method if gameBehavior or healthText is not set
            return;
        }

        int playerHealth = gameBehavior._playerHP;
        // Update health text
        healthText.text = Mathf.RoundToInt(playerHealth) + "%";

        if (playerHealth < 0)
        {
            healthText.text = 0 + "%";
        } 
    }



}
