using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

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
    public RatManager[] m_Rats;
    public GameObject m_RatPrefab;
    
 

    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private static int m_MazeLength = 8;




    // Use this for initialization
    void Start() {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        CreateMaze();
        SpawnAllRats();
    }

    private void CreateMaze(){
        int z = 0;
       // int rX = 90; //eje x rotation
        int rZ = 0;  //eje z rotation
        Vector3 newPosition = new Vector3();
        Quaternion newRotation = new Quaternion();
        //Instance the tiles of the maze
        for (int c=0; c<m_MazeLength; c++)
        { 
            for(int i=0; i<m_MazeLength; i++)
            {
                newPosition.x = m_Tile.transform.position.x + i * 10;
                newPosition.z = z;
                
				GameObject tile = Instantiate(m_Tile, newPosition, newRotation);
                tile.GetComponent<TileManager>().setPosition(c,i);
                if (c == 0 && i == 0)
                {
                    newPosition.x = m_Tile.transform.position.x + i * 10;
                    newPosition.z = z;
                    newPosition.y = 2.5f;
                    m_SpawnList[0].transform.SetPositionAndRotation(newPosition, newRotation);

                }
                if (c == 0 && i == m_MazeLength-1)
                {
                    newPosition.x = m_Tile.transform.position.x + i * 10;
                    newPosition.z = z;
                    newPosition.y = 2.5f;
                    m_SpawnList[1].transform.SetPositionAndRotation(newPosition, newRotation);
                }
                if (c == m_MazeLength-1 && i == 0)
                {
                    newPosition.x = m_Tile.transform.position.x + i * 10;
                    newPosition.z = z;
                    newPosition.y = 2.5f;
                    m_SpawnList[2].transform.SetPositionAndRotation(newPosition, newRotation);
                }
                if (c == m_MazeLength-1 && i == m_MazeLength-1)
                {
                    newPosition.x = m_Tile.transform.position.x + i * 10;
                    newPosition.z = z;
                    newPosition.y = 2.5f;
                    m_SpawnList[3].transform.SetPositionAndRotation(newPosition, newRotation);
                }
                newPosition.y = 0f;
            }
            z = z + 10;
        }
        //Instance the walls of the maze
        newPosition.y = 5;

        newPosition.x = 35;
        newPosition.z = -5;
        newRotation = Quaternion.Euler(90f,0f,rZ);
        Instantiate(m_Border, newPosition, newRotation);

        rZ = 270;
        newPosition.x = -5;
        newPosition.z = 35;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);
        
        rZ = 180;
        newPosition.x = 35;
        newPosition.z = 75;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);
        
        rZ = 90;
        newPosition.x = 75;
        newPosition.z = 35;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        Instantiate(m_Border, newPosition, newRotation);
    }

    private void SpawnAllRats()
    {
        
        for (int i = 0; i < m_Rats.Length; i++)
        {
           
            m_Rats[i].m_Instance =
                Instantiate(m_RatPrefab, m_SpawnList[i].transform.position, m_SpawnList[i].transform.rotation) as GameObject;
            m_Rats[i].m_PlayerNumber = i + 1;
            m_Rats[i].Setup();
        }
        
    }

    // Update is called once per frame
    private IEnumerator GameLoop()
    {
		
	}

    private IEnumerator RoundStarting()
    {
        yield return m_StartWait;
    }

}
