using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject FinishLine;
    [SerializeField] private PlayerController Player;
    [SerializeField] private float ObstacleProximity;

    private bool _cameraMoved;
    private Vector3 _checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        _checkPoint = Player.transform.position;

        _cameraMoved = false;

        Player.OnReachedFinishLine += HandleReachedFinishLine;
        Player.OnPlayerKilled += HandlePlayerKilled;

        UIManager.Instance.OnKilledMessageComplete += HandleKilledMessageComplete;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraOnObstacles();
    }

    private void MoveCameraOnObstacles()
    {
        Obstacle ob = ObstacleManager.Instance.IsNearObstacle(Player.transform.position);

        if(ob != null)
        {
            _cameraMoved = true;
            UIManager.Instance.RepositionCameraOnObstacle(ob);
        }
        else
        {
            if(_cameraMoved)
            {
                //_checkPoint = Player.transform.position;
            }
            _cameraMoved = false;
            UIManager.Instance.ResetCamera();
        }
    }

    private void HandlePlayerKilled()
    {
        UIManager.Instance.DisplayGameOver();
    }

    private void HandleKilledMessageComplete()
    {
        Player = FindObjectOfType<PlayerController>();
        Player.ResetPlayer();
        Player.transform.position = _checkPoint;
        Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        UIManager.Instance.ResetCamera();
    }

    private void HandleReachedFinishLine()
    {
        StartCoroutine(FinishLevel());
    }

    private IEnumerator FinishLevel()
    {
        UIManager.Instance.SetCongratulationsActive(true);

        yield return new WaitForSeconds(2f);

        UIManager.Instance.SetCongratulationsActive(false);
        GameManager.Instance.LoadNextLevel();
    }

    public Vector3 GetFinishPosition()
    {
        return FinishLine.transform.position;
    }
}
