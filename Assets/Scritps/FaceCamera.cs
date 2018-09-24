using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

    public Camera m_camera;
	// Use this for initialization
	void Start () {
	    if(m_camera == null)
        {
            m_camera = Camera.main;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + m_camera.transform.rotation * Vector3.forward, m_camera.transform.rotation * Vector3.up);
	}
}
