using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour
{
    public GameObject ballPrefab;
    private List<GameObject> _currentBalls;

    public NavMeshAgent meshAgent;
    public Transform destination;
    public ParticleSystem destinationPs;

    public static ClickToMove instance;

    private bool _forceStopFlag;

    public CharacterAnimationBehaviour characterAnimation;

    public Transform startDest;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        _currentBalls = new List<GameObject>();

        if (startDest != null)
        {
            destination.transform.position = startDest.position;
            GoToDestination();
        }
    }

    private void Update()
    {
        CheckStopNav();
    }

    void CheckStopNav()
    {
        if (meshAgent.remainingDistance < 0.05f && meshAgent.desiredVelocity.magnitude < 0.15f)
        {
            characterAnimation.StopWalk();
        }
    }

    public void GoToDestination()
    {
        //ClearLastPath();
        meshAgent.SetDestination(destination.position);
        characterAnimation.StartWalk();
        //DrawPath();
    }

    void DrawPath()
    {
        var allDots = meshAgent.path.corners;
        foreach (var dot in allDots)
        {
            //Debug.Log(dot);
            var dotSphere = Instantiate(ballPrefab, dot, Quaternion.identity, transform.parent);
            dotSphere.SetActive(true);
            _currentBalls.Add(dotSphere);
        }
    }

    void ClearLastPath()
    {
        foreach (var ball in _currentBalls)
        {
            Destroy(ball);
        }
        _currentBalls = new List<GameObject>();
    }

    public void ForceStop(bool b)
    {
        _forceStopFlag = b;
        meshAgent.isStopped = b;

        if (_forceStopFlag)
        {
            characterAnimation.StopWalk();
        }
    }

    public void Stop()
    {
        meshAgent.SetDestination(transform.position);
    }

    public void CheckClick()
    {
        if (_forceStopFlag)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                destination.transform.position = hit.point;
                destinationPs.Play();
                GoToDestination();
                com.SoundService.instance.Play("moveClk");
            }
        }
    }
}
