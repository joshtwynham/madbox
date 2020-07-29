using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerController : MonoBehaviour
{
    #region Fields

    public event Action OnReachedFinishLine;
    public event Action OnPlayerKilled;
    public event Action OnApproachObstacle;

    private NavMeshAgent _navMesh;
    private Rigidbody _rigidbody;
    private bool _reachedDestination;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _reachedDestination = false;

        _navMesh = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();

        StopPlayer();
        _navMesh.destination = LevelManager.Instance.GetFinishPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(_navMesh.enabled)
        {
            HandleInput();

            if (_navMesh.remainingDistance < 0.2 && !_reachedDestination)
            {
                ReachedFinishLine();
            }
        }
    }

    public void ResetPlayer()
    {
        _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        _rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);

        _navMesh.enabled = true;
        _navMesh.isStopped = true;
        _navMesh.destination = LevelManager.Instance.GetFinishPosition();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MovePlayer();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopPlayer();
        }
    }

    private void MovePlayer()
    {
        _navMesh.isStopped = false;
    }

    private void StopPlayer()
    {
        _navMesh.isStopped = true;
    }

    private void ReachedFinishLine()
    {
        _reachedDestination = true;
        StopPlayer();

        _navMesh.updatePosition = false;
        OnReachedFinishLine.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Obstacle>() != null && _navMesh.enabled == true)
        {
            KillPlayer();
            PushPlayer(collision);
            OnPlayerKilled.Invoke();
        }
    }

    private void PushPlayer(Collision collision)
    {
        int magnitude = 100;
        Vector3 force = transform.position - collision.transform.position;

        force.Normalize();
        gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);
    }

    public void KillPlayer()
    {
        StopPlayer();
        _navMesh.enabled = false;
    }
}
