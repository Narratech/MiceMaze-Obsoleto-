using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MouseMovement : NetworkBehaviour{


    [SyncVar]
    public int m_PlayerNumber = 0;//1;
    public GameObject manager;

<<<<<<< HEAD
	[SyncVar]
	public Color mi_color = Color.red;

=======
>>>>>>> a5a0f28d56e75161bfeaf7632a6a9d97e254e8e8
	public Text jugador;

    [SyncVar]
    int mi_turno;
    

    public bool move = false;
    public bool te_has_movido = false;


    private Rigidbody m_Rigidbody;

    public override void OnStartLocalPlayer()
    {
		GetComponent<MeshRenderer> ().material.color = mi_color;
        //GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void Start()
    {
		Renderer[] rends = GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rends)
			r.material.color = mi_color;
		
        manager = GameObject.Find("GameManager");
        manager.GetComponent<GameManager>().IncrementaRatones();
        m_PlayerNumber = manager.GetComponent<GameManager>().contadorRatones;
        manager.GetComponent<GameManager>().m_Mouses[m_PlayerNumber - 1] = m_Rigidbody.gameObject;
		jugador.color = Color.red;
		jugador.text = "Jugador: " + m_PlayerNumber;
		jugador.enabled = true;
    }

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

    public void moverse()
    {
        this.move = true;
    }

    [ClientRpc]
    void RpcNotificarMovimiento()
    {
        
        manager.GetComponent<GameManager>().CambiarTurno();
    }

    [Command]
    void CmdNotificarMovimiento()
    {
        RpcNotificarMovimiento();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray;
        int layerMask = 1 << 8;
        Vector3 pos = new Vector3();

        if (!isLocalPlayer)
        {
            return;
        }

        if(m_PlayerNumber != manager.GetComponent<GameManager>().turno)
        {
            return;
        }
       
        if (Input.GetMouseButtonDown(0))
        {

            
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                pos.x = (int) m_Rigidbody.position.x / 10;
                pos.y = 2.5f;
                pos.z = (int)m_Rigidbody.position.z / 10;
                if(hit.collider.gameObject.layer == 8)
                {
                    if(Move(hit.collider.gameObject, pos))
                    {
                        if (isServer)
                            RpcNotificarMovimiento();
                        else
                            CmdNotificarMovimiento();
                        
                    }
                }
               
            }
        }
    }

    public bool Move(GameObject tile, Vector3 position)
    {
        bool moved = false;
        GameObject contains = tile.GetComponent<TileManager>().contains;
        Vector3 pos = tile.GetComponent<TileManager>().GetPosition();
        pos.y = 2.5f;
     
        if (contains == null)
        {
            if (position.z + 1 == pos.z && position.x == pos.x)
            {
                StartCoroutine(MoveAnimation(0, 1, tile));
                moved = true;
            }
            if (position.z - 1 == pos.z && position.x == pos.x)
            {
                StartCoroutine(MoveAnimation(0, -1, tile));
                moved = true;
            }
            if (position.z == pos.z && position.x + 1 == pos.x)
            {
                StartCoroutine(MoveAnimation(1, 0, tile));
                moved = true;
            }
            if (position.z == pos.z && position.x - 1 == pos.x)
            {
                StartCoroutine(MoveAnimation(-1, 0, tile));
                moved = true;
            }
           

        }
        
        return moved;
    }

    private IEnumerator MoveAnimation(float x, float z, GameObject tile)
    {
        for(int c=0; c<10; c++)
        {
            Vector3 pos = m_Rigidbody.transform.position;
            pos.Set(pos.x + x, pos.y, pos.z + z);
            m_Rigidbody.transform.SetPositionAndRotation(pos, m_Rigidbody.transform.rotation);
            yield return 0.1;
        }
        Vector3 posTile = tile.transform.position;
        posTile.y = 2.5f;
        m_Rigidbody.transform.SetPositionAndRotation(posTile, tile.transform.rotation);
    }

    public void DisableControl()
    {
        this.enabled = false;
        move = false;
    }

    public void EnableControl()
    {
        this.enabled = true;

    }

}
