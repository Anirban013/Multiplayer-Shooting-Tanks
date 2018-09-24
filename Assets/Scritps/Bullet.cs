using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Bullet : NetworkBehaviour {

    public int m_speed = 100;
    List<ParticleSystem> m_allParticles;
    public List<string> m_bounceTags;
    public int m_bounce;
    public float m_lifetime = 5f;
    Rigidbody m_rigidbody;
    Collider coll;
    public ParticleSystem m_destroyFX;
	// Use this for initialization
	void Start () {
        m_allParticles = GetComponentsInChildren<ParticleSystem>().ToList();
        m_rigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        StartCoroutine("SelfDestruct"); 
	}
	void OnCollisionExit(Collision col)
    {
        transform.rotation = Quaternion.LookRotation(m_rigidbody.velocity);
    }

    void OnCollisionEnter(Collision col)
    {
        if (m_bounceTags.Contains(col.gameObject.tag))
        {
            if (m_bounce <= 0)
            {
                Explode();
            }
            else
                m_bounce--;
        }
    }


    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(m_lifetime);
        Explode();
    }
    void Explode()
    {
        coll.enabled = false;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.Sleep();
        foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
        {
            m.enabled = false;
        }
        foreach (ParticleSystem m in m_allParticles)
        {
            m.Stop();
        }

        if (m_destroyFX != null)
        {
            m_destroyFX.transform.parent = null;
            m_destroyFX.Play();
        }
        Destroy(gameObject);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
