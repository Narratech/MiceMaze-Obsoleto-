using System.Collections;
using UnityEngine;





public class GameManager : MonoBehaviour
{

    public int m_NumTurnos;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public GameObject m_Tile;
    public GameObject m_Border;
    public GameObject m_WallI;
    public GameObject m_WallL;
    public GameObject m_WallT;
    public GameObject m_WallX;
    public GameObject[] m_SpawnList = new GameObject[4];
    public MouseManager[] m_Mouse;
    public GameObject m_MousePrefab;


    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private static int m_MazeLength = 10;
    




    // Use this for initialization
    void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        CreateMaze();
        //SpawnAllMouses();

        //StartCoroutine(GameLoop());
    }

    private void CreateMaze()
    {
        int z = 0;
        // int rX = 90; //eje x rotation
        int rZ = 0;  //eje z rotation

        var maze = Maze.Load();
        Vector3 newPosition = new Vector3();
        Quaternion newRotation = new Quaternion();
        //Instance the tiles of the maze
        int contSpawns = 0;
        for (int cont = 0; cont < m_MazeLength*m_MazeLength; cont++)
        {
           
            
                newPosition.x = maze.Tiles[cont].X * 10;
                newPosition.z = maze.Tiles[cont].Z * 10;

                GameObject tile = Instantiate(m_Tile, newPosition, newRotation);
                tile.GetComponent<TileManager>().SetPosition(maze.Tiles[cont].X, maze.Tiles[cont].Z);
               
                switch (maze.Tiles[cont].Contains)
                {
                    case "Empty": break;
                    case "Spawn":
                       
                        newPosition.y = 2.5f;
                        m_SpawnList[contSpawns].transform.SetPositionAndRotation(newPosition, newRotation);
                        contSpawns++;
                        newPosition.y = 0f;
                        break;
                    case "WallI":
                        newPosition.y = 5f;
                        newRotation = Quaternion.Euler(0, maze.Tiles[cont].Rotation, 0);
                        GameObject wallI = Instantiate(m_WallI, newPosition, newRotation);
                        tile.GetComponent<TileManager>().SetContains(wallI);
                        newPosition.y = 0f;
                        break;
                    case "WallL":
                        newPosition.y = 5f;
                        newRotation = Quaternion.Euler(0, maze.Tiles[cont].Rotation, 0);
                        GameObject wallL = Instantiate(m_WallL, newPosition, newRotation);
                        tile.GetComponent<TileManager>().SetContains(wallL);
                        newPosition.y = 0f;
                        break;
                    case "WallT":
                        newPosition.y = 5f;
                        newRotation = Quaternion.Euler(0, maze.Tiles[cont].Rotation, 0);
                        GameObject wallT = Instantiate(m_WallT, newPosition, newRotation);
                        tile.GetComponent<TileManager>().SetContains(wallT);
                        newPosition.y = 0f;
                        break;
                    case "WallX":
                        newPosition.y = 5f;
                        newRotation = Quaternion.Euler(0, maze.Tiles[cont].Rotation, 0);
                        GameObject wallX = Instantiate(m_WallX, newPosition, newRotation);
                        tile.GetComponent<TileManager>().SetContains(wallX);
                        newPosition.y = 0f;
                        break;

                }
               
                newRotation = Quaternion.Euler(0, 0, 0);
                newPosition.y = 0f;
            
        }
        //Instance the walls of the maze
        newPosition.y = 5;

        newPosition.x = (m_MazeLength * 10 / 2) - 5;
        newPosition.z = -5;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);

        rZ = 270;
        newPosition.x = -5;
        newPosition.z = (m_MazeLength * 10 / 2) - 5;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);

        rZ = 180;
        newPosition.x = (m_MazeLength * 10 / 2) - 5;
        newPosition.z = (m_MazeLength * 10) - 5;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);

        rZ = 90;
        newPosition.x = (m_MazeLength * 10) - 5;
        newPosition.z = (m_MazeLength * 10 / 2) - 5;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);
    }

    private void SpawnAllMouses()
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

    }

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
