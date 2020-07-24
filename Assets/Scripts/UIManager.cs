using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _congratulations;
    [SerializeField] private Text _gameOver;
    [SerializeField] private CameraFollow _camera;

    public void DisplayCongratulations()
    {
        _congratulations.gameObject.SetActive(true);
    }

    public void DisplayGameOver()
    {
        DetachCamera();
        _gameOver.gameObject.SetActive(true);
    }

    public void RepositionCameraOnObstacle(Obstacle obstacle)
    {
        _camera.RepositionCameraOnObstacle(obstacle);
    }

    public void ResetCamera()
    {
        _camera.Reposition();
    }

    private void DetachCamera()
    {
        _camera.DetachCameraFromPlayer();
    }
}
