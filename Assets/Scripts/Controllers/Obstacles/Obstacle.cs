using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public float MovementSpeed;

    virtual protected void Update()
    {
        MoveObstacle();
    }

    protected abstract void MoveObstacle();
}
