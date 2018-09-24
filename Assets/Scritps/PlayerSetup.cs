using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
public class PlayerSetup : NetworkBehaviour {

    public Color m_playerColor;
    public string m_baseName = "PLAYER";
    public int m_playerNum = 1;
    public Text m_playerName;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (m_playerName != null)
        {
            m_playerName.enabled = false;
            
        }
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer r in meshes)
        {
            r.material.color = m_playerColor;
        }
        if(m_playerName != null)
        {
            m_playerName.enabled = true;
            m_playerName.text = m_baseName + m_playerNum.ToString();
        }
    }
}
