using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour {

    Rigidbody m_rigidbody;

    public Transform m_chasis;
    public Transform m_turret;
    public float m_maxSpeed = 100f;

    public float m_chasisRotateSpeed = 1f;
    public float m_turretRotateSpeed = 3f;

   // public Vector3 m_turretDirection;
   // public Vector3 m_chasisDirection;
    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MovePlayer(Vector3 dir)
    {
        Vector3 moveDirection = dir * m_maxSpeed * Time.deltaTime;
        m_rigidbody.velocity = moveDirection;
    }

    public void FaceDirection(Transform xform, Vector3 dir, float rotSpeed)
    {
        if (dir != Vector3.zero && xform != null)
        {
            Quaternion desiredRot = Quaternion.LookRotation(dir);
            xform.rotation = Quaternion.Slerp(xform.rotation, desiredRot, rotSpeed * Time.deltaTime);
        }
    }

    public void RotateChasis(Vector3 dir)
    {
        FaceDirection(m_chasis, dir, m_chasisRotateSpeed);
    }
    public void RotateTurret(Vector3 dir)
    {
        FaceDirection(m_turret, dir, m_turretRotateSpeed);
    }
}
