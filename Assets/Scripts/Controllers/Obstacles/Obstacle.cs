using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public float MovementSpeed;

    abstract public float ObstacleProximity { get; }

    virtual protected void Start()
    {
        ObstacleManager.Instance.RegisterObstacle(this);
    }

    virtual protected void FixedUpdate()
    {
        MoveObstacle();
    }

    protected abstract void MoveObstacle();
}
