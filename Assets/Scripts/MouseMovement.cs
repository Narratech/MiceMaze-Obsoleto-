using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour{


    public int m_PlayerNumber = 1;



    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    public bool Move(GameObject tile, Vector3 position)
    {
        bool moved = false;
        GameObject contains = tile.GetComponent<TileManager>().contains;
        Vector3 pos = tile.transform.position;
        pos.y = 2.5f;
        
        if (contains == null)
        {
            if (position.z + 10 == pos.z && position.x == pos.x)
            {
                m_Rigidbody.transform.SetPositionAndRotation(pos, tile.transform.rotation);
                moved = true;
            }
            if (position.z - 10 == pos.z && position.x == pos.x)
            {
                m_Rigidbody.transform.SetPositionAndRotation(pos, tile.transform.rotation);
                moved = true;
            }
            if (position.z == pos.z && position.x + 10 == pos.x)
            {
                m_Rigidbody.transform.SetPositionAndRotation(pos, tile.transform.rotation);
                moved = true;
            }
            if (position.z == pos.z && position.x - 10 == pos.x)
            {
                m_Rigidbody.transform.SetPositionAndRotation(pos, tile.transform.rotation);
                moved = true;
            }
        }
        
        return moved;
    }


}
