using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	private Vector2 position;
	public GameObject contains;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void setPosition(int x, int y)
	{
		position.x = x;
		position.y = y;
	}

	public Vector2 getPosition()
	{
		return position;
	}

	public void setContains(GameObject gameObject)
	{
		contains = gameObject;
	}

	public GameObject getContains()
	{
		return contains;
	}
}
