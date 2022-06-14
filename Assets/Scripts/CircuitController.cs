using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitController : MonoBehaviour
{

    public int WaypointsCount = 3;

    [SerializeField]
    private int WaypointsLeft;

    public bool CircuitFinished { get; private set; }

    private void Start()
    {
        CircuitFinished = false;

        WaypointsLeft = WaypointsCount;
    }

    public void UpdateWaypointsLeft()
    {
        WaypointsLeft--;

        CheckIfFinished();
    }

    private void CheckIfFinished()
    {
        if (WaypointsLeft == 0)
        {
            CircuitFinished = true;
        }
    }

}
