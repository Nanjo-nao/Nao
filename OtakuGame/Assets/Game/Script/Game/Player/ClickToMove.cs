using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClickToMove : MonoBehaviour
{
    public GameObject ballPrefab;
    private List<GameObject> _currentBalls;

    public NavMeshAgent meshAgent;
    public Transform destination;

    public float timeToThink = 1;
    private float _timer;


    public void Start()
    {
        _currentBalls = new List<GameObject>();
        _timer = 0;
    }

    public void Update()
    {
        _timer = _timer - Time.deltaTime;

        if (_timer<=0)
        {
            GoToDestination();
            _timer = timeToThink;
        }

        CheckClick();
    }

    public void GoToDestination()
    {
        ClearLastPath();
        meshAgent.SetDestination(destination.position);
        DrawPath();
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

    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 6)
                {
                    destination.transform.position = hit.point;
                }
            }
        }
    }
}