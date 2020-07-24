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

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();


        StopPlayer();
        _navMesh.destination = GameManager.Instance.GetFinishPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(_navMesh.enabled)
        {
            HandleInput();

            if (_navMesh.remainingDistance < 0.2)
            {
                ReachedFinishLine();
            }
        }
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
        StopPlayer();

        _navMesh.updatePosition = false;
        OnReachedFinishLine.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Obstacle>() != null && _navMesh.enabled == true)
        {
            OnPlayerKilled.Invoke();

            PushPlayer(collision);
            KillPlayer();
        }
    }

    private void PushPlayer(Collision collision)
    {
        int magnitude = 1;
        Vector3 force = transform.position - collision.transform.position;

        force.Normalize();
        gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);
    }

    private void KillPlayer()
    {
        StopPlayer();
        _navMesh.enabled = false;
    }
}
