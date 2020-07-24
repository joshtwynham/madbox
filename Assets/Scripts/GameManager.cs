using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject FinishLine;
    public PlayerController Player;
    public Obstacle[] obstacles;

    public float ObstacleProximity;

    private bool _cameraMoved;

    // Start is called before the first frame update
    void Start()
    {
        _cameraMoved = false;

        Player.OnReachedFinishLine += HandleReachedFinishLine;
        Player.OnPlayerKilled += HandlePlayerKilled;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCameraOnObstacles();
    }

    private void MoveCameraOnObstacles()
    {
        Obstacle ob = OnObstacle();

        if(ob != null)
        {
            _cameraMoved = true;
            UIManager.Instance.RepositionCameraOnObstacle(ob);
        }
        else
        {
            _cameraMoved = false;
            UIManager.Instance.ResetCamera();
        }
    }

    // Checks if player is near any obstacles
    // Returns the obstacle the player is near
    private Obstacle OnObstacle()
    {
        Obstacle obstacle = null;

        foreach (Obstacle ob in obstacles)
        {
            if (Vector3.Distance(Player.transform.position, ob.transform.position) < ObstacleProximity)
            {
                obstacle = ob;
            }
        }

        return obstacle;
    }

    private void HandlePlayerKilled()
    {
        UIManager.Instance.DisplayGameOver();
    }

    private void HandleReachedFinishLine()
    {
        UIManager.Instance.DisplayCongratulations();

        Time.timeScale = 0f;
    }

    public Vector3 GetFinishPosition()
    {
        return FinishLine.transform.position;
    }
}
