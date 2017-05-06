using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    private Transform _transform;
    private Transform target;
    public Vector3 offset = new Vector3(3, 7.5f, -3);

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for initialization
    void Start () {
        _transform = this.transform;
	}

    private void FixedUpdate()
    {
        if (target != null)
        {
            _transform.position = target.position + offset;
            _transform.LookAt(target.position, Vector3.up);
        }
    }
}
