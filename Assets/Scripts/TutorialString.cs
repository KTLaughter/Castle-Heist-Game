using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialString : MonoBehaviour
{
    public TMP_Text tutorialText; // Assign this in the Inspector

    public GameBehavior gameBehavior; // Reference to the GameBehavior component

    void Start()
    {
        // Get the GameBehavior component from the GameObject
        gameBehavior = FindObjectOfType<GameBehavior>();
    }



    void Update()
    {
        if (gameBehavior == null || tutorialText == null)
        {
            return;
        }

        string tutorial = gameBehavior.labelText;
        // Update health text
        tutorialText.text = tutorial;

    }

}
