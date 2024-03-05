using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _healthbar1;
    [SerializeField] private GameObject _healthbar2;
    [SerializeField] private GameObject _healthbar3;
    [SerializeField] private GameObject _healthbar4;

    [SerializeField] private GameObject _pauseMenu;

    [SerializeField] private IntVariable _playerCurrentHP;
    public static bool gameIsPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void FixedUpdate()
    {
        switch (_playerCurrentHP.value)
        {
            case 4:
                _healthbar1.SetActive(false);
                break;
            case 3:
                _healthbar2.SetActive(false);
                break;
            case 2:
                _healthbar3.SetActive(false);
                break;
            case 1:
                _healthbar4.SetActive(false);
                break;
        }
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
