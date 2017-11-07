using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public int m_NumTurnos;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public GameObject m_Tile;
    public GameObject m_Wall;
    public GameObject m_RatPrefab;
    //public RatManager[] m_Rats;

    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private int m_MazeLength = 8;

    

    // Use this for initialization
    void Start() {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        CreateMaze();
        SpawnAllRats();
    }

    private void CreateMaze(){
        int z = 0;
        int rX = 90; //eje x rotation
        int rZ = 0;  //eje z rotation
        Vector3 newPosition = new Vector3();
        Quaternion newRotation = new Quaternion();
        //Instance the tiles of the maze
        for (int c=0; c<m_MazeLength; c++)
        {
            for(int i=0; i<m_MazeLength; i++)
            {
                newPosition.x = m_Tile.transform.position.x + i * 11;
                newPosition.z = z;
                GameObject tileInstance = Instantiate(m_Tile, newPosition, newRotation);
            }
            z = z + 11;
        }
        //Instance th walls of the maze
        newPosition.y = 5;

        newPosition.x = 39;
        newPosition.z = -6;
        newRotation = Quaternion.Euler(90f,0f,rZ);
        GameObject wallInstance = Instantiate(m_Wall, newPosition, newRotation);

        rZ = 270;
        newPosition.x = -6;
        newPosition.z = 39;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        GameObject wallInstance1 = Instantiate(m_Wall, newPosition, newRotation);
        
        rZ = 180;
        newPosition.x = 39;
        newPosition.z = 83;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        GameObject wallInstance2 = Instantiate(m_Wall, newPosition, newRotation);
        
        rZ = 90;
        newPosition.x = 83;
        newPosition.z = 39;
        newRotation = Quaternion.Euler(90f, 0f, rZ);
        GameObject wallInstance3 = Instantiate(m_Wall, newPosition, newRotation);
    }

    private void SpawnAllRats()
    {
        /* 
        for (int i = 0; i < m_Rats.Length; i++)
        {
           
            m_Rats[i].m_Instance =
                Instantiate(m_RatPrefab, m_Rats[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }
        */
    }

    // Update is called once per frame
    void Update () {
		
	}
}
