using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : Obstacle
{
    public override float ObstacleProximity { get { return transform.lossyScale.x / 0.5f; } }

    private float _defaultRange = 1.5f;
    private Vector3 _initialPosition;
    private Vector3 _movementDirection;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        _initialPosition = transform.position;
        _movementDirection = Vector3.left;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        DetermineDirection();
        base.FixedUpdate();
    }

    private void DetermineDirection()
    {
        if (transform.position.x < -_defaultRange)
        {
            _movementDirection = Vector3.right;
        }
        else if (transform.position.x > _defaultRange)
        {
            _movementDirection = Vector3.left;
        }
    }

    protected override void MoveObstacle()
    {
        transform.Translate(_movementDirection * MovementSpeed * Time.deltaTime);
    }
}
