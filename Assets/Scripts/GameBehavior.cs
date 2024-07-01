using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{
    Vector2 nativeSize = new Vector2(640, 480);
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string labelText = "Destroy all of the generators to break the shield!";
    public bool winCondition = false;
    public int _playerHP = 100;
    GUIStyle healthFont;

    void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
 
    public void ScreenStuff(string label)
    {
        labelText = label;
        Time.timeScale = 0f;
    }

    
    public int Items
    {
        get { return _playerHP; }
        
        set
        {
            _playerHP = value;
            if(_playerHP <= 100)
            {
              //  ScreenStuff("HP+ 10");
                Time.timeScale = 1;
                if (_playerHP >= 100)
                {
                    _playerHP = 100;
                }

                if (_playerHP <= 0)
                {
                    _playerHP = 0;
                }
            }
        }
    }

    public bool DidYouWin
    {
        get { return winCondition; }

        set
        {
            winCondition = value;
            if (DidYouWin == true)
            {
                FindObjectOfType<AudioManager>().Play("Victory");
                Time.timeScale = 0;
                showWinScreen = true;
            }
        }
    }

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            if(_playerHP <= 0)
            {
                FindObjectOfType<AudioManager>().Play("Defeat");
                ScreenStuff("Ouch... that's gotta hurt.");
                showLossScreen = true;
            }
        }
    }

    void OnGUI()
    {
        healthFont = new GUIStyle();
        healthFont.fontSize = (int)(12.5f * ((float)Screen.width / (float)nativeSize.x));
        healthFont.normal.textColor = Color.white;
        //GUI.Label(new Rect(70, 40, 150, 25), _playerHP + "%", healthFont);
        //GUI.Label(new Rect(Screen.width / 2 - 750, Screen.height - 100, 150, 100), labelText, healthFont);

        if (showWinScreen)
        {
            healthFont.alignment = TextAnchor.UpperCenter;
            healthFont.fontSize = (int)(20f * ((float)Screen.width / (float)nativeSize.x));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "VICTORY!", healthFont);
            if (Input.GetMouseButtonDown(0))
            {
                RestartLevel();
            }
        }

        if (showLossScreen)
        {
            healthFont.alignment = TextAnchor.UpperCenter;
            healthFont.fontSize = (int)(20f * ((float)Screen.width / (float)nativeSize.x));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            labelText = string.Empty;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "YOU LOSE..", healthFont);
            if (Input.GetMouseButtonDown(0))
            {
                RestartLevel();
            }
        }
    }
    
}