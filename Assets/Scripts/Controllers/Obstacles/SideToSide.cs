using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : Obstacle
{
    private Vector3 _movementDirection;

    // Start is called before the first frame update
    private void Start()
    {
        _movementDirection = Vector3.left;
    }

    // Update is called once per frame
    protected override void Update()
    {
        DetermineDirection();
        base.Update();
    }

    private void DetermineDirection()
    {
        if (transform.position.x < -1)
        {
            _movementDirection = Vector3.right;
        }
        else if (transform.position.x > 1)
        {
            _movementDirection = Vector3.left;
        }
    }

    protected override void MoveObstacle()
    {
        transform.Translate(_movementDirection * MovementSpeed * Time.deltaTime);
    }
}
