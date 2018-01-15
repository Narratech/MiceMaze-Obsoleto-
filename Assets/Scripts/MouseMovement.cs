using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MouseMovement : NetworkBehaviour{


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

    private void Update()
    {
        RaycastHit hit;
        Ray ray;
        int layerMask = 1 << 8;

        if (!isLocalPlayer )
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask))
            {

                Move(hit.collider.gameObject, m_Rigidbody.position);
                

            }
        }
    }

    public bool Move(GameObject tile, Vector3 position)
    {
        bool moved = false;
        GameObject contains = tile.GetComponent<TileManager>().contains;
        Vector3 pos = tile.transform.position;
        pos.y = 2.5f;
     
        if (contains == null)
        {
            /*if (position.z + 10 == pos.z && position.x == pos.x)
            {
                StartCoroutine(MoveAnimation(0, 0.1f, tile));
                moved = true;
            }
            if (position.z - 10 == pos.z && position.x == pos.x)
            {
                StartCoroutine(MoveAnimation(0, -0.1f, tile));
                moved = true;
            }
            if (position.z == pos.z && position.x + 10 == pos.x)
            {
                StartCoroutine(MoveAnimation(0.1f, 0, tile));
                moved = true;
            }
            if (position.z == pos.z && position.x - 10 == pos.x)
            {
                StartCoroutine(MoveAnimation(-0.1f, 0, tile));
                moved = true;
            }*/
            StartCoroutine(MoveAnimation(-0.1f, 0, tile));

        }
        
        return moved;
    }

    private IEnumerator MoveAnimation(float x, float z, GameObject tile)
    {
        for(int c=0; c<100; c++)
        {
            Vector3 pos = m_Rigidbody.transform.position;
            pos.Set(pos.x + x, pos.y, pos.z + z);
            m_Rigidbody.transform.SetPositionAndRotation(pos, m_Rigidbody.transform.rotation);
            yield return null;
        }
        Vector3 posTile = tile.transform.position;
        posTile.y = 2.5f;
        m_Rigidbody.transform.SetPositionAndRotation(posTile, tile.transform.rotation);
    }
}
