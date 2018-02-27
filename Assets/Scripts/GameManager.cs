using System.Collections;
using UnityEngine;





public class GameManager : MonoBehaviour
{

    public int m_NumTurnos;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public MouseManager[] m_Mouse;
    public GameObject m_MousePrefab;


    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;

    




    // Use this for initialization
    void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

       
        //SpawnAllMouses();

        //StartCoroutine(GameLoop());
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
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        ResetAllMouses();
        DisableMouseControl();
       
        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        bool moved = false;
        Vector3 newPosition;
        RaycastHit hit;
        Ray ray;
        int layerMask = 1 << 8;
        for (int c = 0; c<m_NumTurnos; c++)
        {
            for(int i = 0; i<m_Mouse.Length; i++)
            {
                moved = false;
                EnableMouseControl(i);
                while (!moved)
                {
                   
                    if (Input.GetMouseButtonDown(0))
                    {
                        
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, layerMask))
                        {
                           
                            moved = m_Mouse[i].Move(hit.collider.gameObject, m_Mouse[i].m_Position, out newPosition);
                            m_Mouse[i].SetPosition(newPosition);

                        }
                    }
                    
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
    private void ResetAllMouses()
    {
        for (int i = 0; i < m_Mouse.Length; i++)
        {
            m_Mouse[i].Reset();
        }
    }

    private void EnableMousesControl()
    {
        for (int i = 0; i < m_Mouse.Length; i++)
        {
            m_Mouse[i].EnableControl();
        }
    }

    private void EnableMouseControl(int i)
    {

         m_Mouse[i].EnableControl();

    }

    private void DisableMouseControl()
    {
        for (int i = 0; i < m_Mouse.Length; i++)
        {
            m_Mouse[i].DisableControl();
        }
    }


  
}
