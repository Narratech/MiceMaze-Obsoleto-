using System;
using UnityEngine;

[Serializable]
public class MouseManager{


    public Color m_PlayerColor;

    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public GameObject m_Instance;

    private MouseMovement m_Movement;

    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<MouseMovement>();

        m_Movement.m_PlayerNumber = m_PlayerNumber;

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = m_PlayerColor;
        }
    }

    public void DisableControl()
    {
        m_Movement.enabled = false;

    }

    public void EnableControl()
    {
        m_Movement.enabled = true;

    }

    public void Reset()
    {


        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}

