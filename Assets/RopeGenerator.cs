using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeGenerator : MonoBehaviour
{
    LineRenderer _line;
    List<Transform> _ropeSegments = new List<Transform>();

    void Start()
    {
        _line = GetComponent<LineRenderer>();

        _ropeSegments.Add(transform.GetChild(0));
        List<AutoConnectJoint> ropeSegments = GetComponentsInChildren<AutoConnectJoint>().ToList();
        ropeSegments.ForEach(x => _ropeSegments.Add(x.transform));

        _line.positionCount = _ropeSegments.Count;
    }

    void Update()
    {
        for (int i = 0; i < _ropeSegments.Count; i++)
        {
            _line.SetPosition(i, _ropeSegments[i].transform.position);
        }
    }
}
