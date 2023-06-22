using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject _target;

    private Vector3  _positionOffset;
    // Start is called before the first frame update

    void Start()
    {
        _positionOffset = transform.position - _target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.transform.position + _positionOffset;
    }
}
