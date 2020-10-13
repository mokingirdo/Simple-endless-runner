using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;

    Vector3 _start_distance, _move_vec;

    // Start is called before the first frame update
    void Start()
    {
        _start_distance = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        _move_vec = Target.position + _start_distance;

        _move_vec.z = 0;
        _move_vec.y = _start_distance.y;

        transform.position = _move_vec;
    }
}
