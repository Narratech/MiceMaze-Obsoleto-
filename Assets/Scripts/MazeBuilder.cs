﻿using System;
using UnityEngine;

public class MazeBuilder: MonoBehaviour {



	public GameObject m_Tile;
	public GameObject m_Border;
	public GameObject m_Wall;
	public GameObject[] m_SpawnList = new GameObject[4];
	private static int m_MazeLength = 10;



	void Start(){
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
			case "Wall":
				newPosition.y = 5f;
				newRotation = Quaternion.Euler(0, maze.Tiles[cont].Rotation, 0);
				GameObject wall = Instantiate(m_Wall, newPosition, newRotation);
				
				tile.GetComponent<TileManager>().SetContains(wall);
				newPosition.y = 0f;
				break;
			}

			newRotation = Quaternion.Euler(0, 0, 0);
			newPosition.y = 0f;

		}
		//Instance the walls of the maze
		GameObject border;
		Vector3 scale = new Vector3(m_MazeLength,1,1);
		newPosition.y = 5;

		newPosition.x = (m_MazeLength * 10 / 2) - 5;
		newPosition.z = -5;
		newRotation = Quaternion.Euler(90f, 0f, rZ);

		border = Instantiate(m_Border, newPosition, newRotation);
        border.transform.localScale = scale;

		rZ = 270;
		newPosition.x = -5;
		newPosition.z = (m_MazeLength * 10 / 2) - 5;
		newRotation = Quaternion.Euler(90f, 0f, rZ);

		border = Instantiate(m_Border, newPosition, newRotation);
        border.transform.localScale = scale;

        rZ = 180;
		newPosition.x = (m_MazeLength * 10 / 2) - 5;
		newPosition.z = (m_MazeLength * 10) - 5;
		newRotation = Quaternion.Euler(90f, 0f, rZ);

		border = Instantiate(m_Border, newPosition, newRotation);
        border.transform.localScale = scale;

        rZ = 90;
		newPosition.x = (m_MazeLength * 10) - 5;
		newPosition.z = (m_MazeLength * 10 / 2) - 5;
		newRotation = Quaternion.Euler(90f, 0f, rZ);

		border = Instantiate(m_Border, newPosition, newRotation);
        border.transform.localScale = scale;
    }
		
	
	// Update is called once per frame
	void Update () {
		
	}
}
