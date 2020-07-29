using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : Obstacle
{
    private Vector3 rotation;

    public override float ObstacleProximity { get { return transform.lossyScale.x / 0.15f; } }

    override protected void Start()
    {
        base.Start();
        rotation = new Vector3(0, MovementSpeed, 0);
    }

    protected override void MoveObstacle()
    {
        transform.Rotate(rotation, Space.World);
    }
}
