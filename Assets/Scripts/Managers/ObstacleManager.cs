using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    private List<Obstacle> _obstacles;

    override protected void Awake()
    {
        base.Awake();
        _obstacles = new List<Obstacle>();
    }

    public void RegisterObstacle(Obstacle obstacle)
    {
        _obstacles.Add(obstacle);
    }

    // Checks if there are any Obstacles near the provided position
    // Returns the first Obstacle near the position
    // 'Near the position' is defined by the ObstacleProximity property of Obstacle
    public Obstacle IsNearObstacle(Vector3 position)
    {
        Obstacle obstacle = null;

        foreach (Obstacle ob in _obstacles)
        {
            if (Vector3.Distance(position, ob.transform.position) < ob.ObstacleProximity)
            {
                return ob;
            }
        }

        return obstacle;
    }
}
