using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow  : MonoBehaviour
{
    public Transform Player;
    public Vector3 BaseOffset;
    public float SmoothSpeed;

    private Vector3 _offset;

    private bool _isAttached;
    private bool _isOnObstacle;

    private void Start()
    {
        _isOnObstacle = false;
        _offset = BaseOffset;
        _isAttached = true;
    }

    private void LateUpdate()
    {
        if(_isAttached)
        {
            Vector3 desiredPosition = Player.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

            transform.position = smoothedPosition;
            transform.LookAt(Player);
        }
    }

    // Move Camera to initial position in relation to the player
    public void Reposition()
    {
        _isOnObstacle = false;
        _offset = BaseOffset;
    }

    public void DetachCameraFromPlayer()
    {
        _isAttached = false;
    }

    public void RepositionCameraOnObstacle(Obstacle obstacle)
    {
        if(!_isOnObstacle)
        {
            if (obstacle as Spinning)
            {
                float sizeMultiplier = obstacle.transform.lossyScale.x * 10;
                _offset += new Vector3(sizeMultiplier, sizeMultiplier, 0);
            }
            else if (obstacle as SideToSide)
            {
                _offset += new Vector3(0, 3, 0);
            }

            _isOnObstacle = true;
        }

    }
}
