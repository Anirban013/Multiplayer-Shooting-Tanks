using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour {


    PlayerMotor m_pMotor;
    PlayerHealth m_pHealth;
    PlayerSetup m_pSetup;
    PlayerShoot m_pShoot;
	// Use this for initialization
	void Start () {

        m_pHealth = GetComponent<PlayerHealth>();
        m_pMotor = GetComponent<PlayerMotor>();
        m_pSetup = GetComponent<PlayerSetup>();
        m_pShoot = GetComponent<PlayerShoot>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            m_pShoot.Shoot();
        }
        Vector3 inputDir = GetInput();
        if(inputDir.sqrMagnitude > 0.25f)
        {
            m_pMotor.RotateChasis(inputDir);
        }
        Vector3 turretDir = Utility.GetWorldPointFromScreenPoint(Input.mousePosition, m_pMotor.m_turret.position.y) - m_pMotor.m_turret.position;
        m_pMotor.RotateTurret(turretDir);
    }

    Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector3 inputDir = GetInput();
        m_pMotor.MovePlayer(inputDir);
    }
}
