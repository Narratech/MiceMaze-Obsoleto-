using System.Collections;
using UnityEngine;
using UnityEngine.Networking;




public class GameManager :  NetworkBehaviour
{

    public int m_NumTurnos;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public GameObject[] m_Mouses = new GameObject[5];
    public GameObject m_MousePrefab;


    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    [SyncVar]
    public int turno = 1;

    public int contadorRatones = 0;
    // Use this for initialization

    

    void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        //SpawnAllMouses();

        StartCoroutine(GameLoop());
    }

    public void IncrementaRatones()
    {
        contadorRatones++;
    }

    
    public void CambiarTurno()
    {
        turno++;
        if(turno > contadorRatones)
        {
            turno = 1;
        }
           
    }

    


    /*private void SpawnAllMouses()
    {

        for (int i = 0; i < m_Mouse.Length; i++)
        {

            m_Mouse[i].m_Instance =
                Instantiate(m_MousePrefab, m_SpawnList[i].transform.position, m_SpawnList[i].transform.rotation) as GameObject;
            m_Mouse[i].m_SpawnPoint = m_SpawnList[i].transform;
            Vector3 pos = new Vector3(m_SpawnList[i].transform.position.x/10, m_SpawnList[i].transform.position.y, m_SpawnList[i].transform.position.z / 10);
            m_Mouse[i].SetPosition(pos);
            m_Mouse[i].m_PlayerNumber = i + 1;
            m_Mouse[i].Setup();
        }

    }*/


    // Update is called once per frame
    private IEnumerator GameLoop()
    {
        print("hola");
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        
        DisableMouseControl();
       
        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        for (int c = 0; c<m_NumTurnos; c++)
        {
            for(int i = 0; i<contadorRatones; i++)
            {
                EnableMouseControl(i);
                while (!m_Mouses[i].GetComponent<MouseMovement>().move)
                {
                    yield return null;
                }
                DisableMouseControl();
            }
        }
       
    }

    private IEnumerator RoundEnding()
    {
        DisableMouseControl();

        yield return m_EndWait;
    }

    // This function is used to turn all the tanks back on and reset their positions and properties.
 

    private void EnableMousesControl()
    {
        for (int i = 0; i < contadorRatones; i++)
        {
            m_Mouses[i].GetComponent<MouseMovement>().EnableControl();
        }
    }

    private void EnableMouseControl(int i)
    {

         //m_Mouses[i].GetComponent<MouseMovement>().EnableControl();

    }

    private void DisableMouseControl()
    {
        for (int i = 0; i < contadorRatones; i++)
        {
           //m_Mouses[i].GetComponent<MouseMovement>().DisableControl();
        }
    }
    

  
}
