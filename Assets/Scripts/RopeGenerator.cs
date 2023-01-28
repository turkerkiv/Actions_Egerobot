using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeGenerator : MonoBehaviour
{
    // [SerializeField] Transform _segmentPrefab;
    // [SerializeField] Transform _handle;
    // [SerializeField] int _ropeSegmentCount = 10;
    // [SerializeField] float _ropeSegmentLength = 0.5f;

    LineRenderer _line;
    List<Transform> _ropeSegments = new List<Transform>();

    // public Transform FirstSegment => _ropeSegments[0];
    // public Transform LastSegment => _ropeSegments[_ropeSegments.Count - 1];

    void Awake()
    {
        _line = GetComponent<LineRenderer>();

        _ropeSegments.Add(transform.GetChild(0));
        List<AutoConnectJoint> ropeSegments = GetComponentsInChildren<AutoConnectJoint>().ToList();
        ropeSegments.ForEach(x => _ropeSegments.Add(x.transform));

        // _handle.position = transform.position + (_ropeSegmentCount - 1) * _ropeSegmentLength * Vector3.down;

        _line.positionCount = _ropeSegments.Count;
    }

    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < _ropeSegments.Count; i++)
        {
            _line.SetPosition(i, _ropeSegments[i].transform.position);
        }
    }

    //does not work right now
    // void GeneratePoints()
    // {
    //     //add first one
    //     Transform firstSegment = Instantiate(_segmentPrefab, transform.position, Quaternion.identity, transform);
    //     firstSegment.GetComponent<Rigidbody>().isKinematic = true;
    //     _ropeSegments.Add(firstSegment);

    //     //add the rest
    //     for (int i = 1; i < _ropeSegmentCount; i++)
    //     {
    //         Vector3 pos = transform.position + Vector3.down * i * _ropeSegmentLength;
    //         Transform segment = Instantiate(_segmentPrefab, pos, Quaternion.identity, _ropeSegments[i - 1].transform);
    //         segment.GetComponent<Rigidbody>().isKinematic = true;
    //         segment.gameObject.AddComponent<AutoConnectJoint>();
    //         _ropeSegments.Add(segment);
    //     }
    // }
}
