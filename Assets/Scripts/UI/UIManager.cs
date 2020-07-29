using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _congratulations;
    [SerializeField] private Text _gameOver;
    [SerializeField] private CameraFollow _playerCamera;
    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelName _levelName;

    public event Action OnKilledMessageComplete;

    private void Start()
    {
        GameManager.Instance.OnLevelLoaded += HandleLevelLoaded;
    }

    private void HandleLevelLoaded()
    {
        _levelName.SetLevelText(FormatLevelName(GameManager.Instance.CurrentLevelName));
        _levelName.FadeOut();
        _playerCamera = FindObjectOfType<CameraFollow>();
    }

    private string FormatLevelName(string levelName)
    {
        string addedSpaces = string.Concat(levelName.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

        return "'" + addedSpaces + "'";
    }

    public void DisplayMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        SetDummyCameraActive(true);
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

    public void SetCongratulationsActive(bool active)
    {
        _congratulations.gameObject.SetActive(active);
    }

    public void DisplayGameOver()
    {
        StartCoroutine(DisplayPlayerKilledMessage());
    }

    public void RepositionCameraOnObstacle(Obstacle obstacle)
    {
        _playerCamera.RepositionCameraOnObstacle(obstacle);
    }

    public void ResetCamera()
    {
        _playerCamera.Reposition();
    }

    private IEnumerator DisplayPlayerKilledMessage()
    {
        _gameOver.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        _gameOver.gameObject.SetActive(false);

        if(OnKilledMessageComplete != null)
        {
            OnKilledMessageComplete.Invoke();
        }
    }

    private void DetachCamera()
    {
        _playerCamera.DetachCameraFromPlayer();
    }
}
