using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesMotion : MonoBehaviour
{

    LineRenderer Line;

    public Transform Origin;
    public Transform Destination;

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();
        Line.startWidth = 0.3f;
        Line.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Line.SetPosition(0, Origin.position);
        Line.SetPosition(1, Destination.position);
    }
}
