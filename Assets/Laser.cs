using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public LineRenderer lineRenderer;
    public Transform cube1;
    public Transform cube2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        lineRenderer.SetPosition(0, cube1.position);
        lineRenderer.SetPosition(1, cube2.position);
	}
}
