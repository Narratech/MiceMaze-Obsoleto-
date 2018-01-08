using UnityEngine;

public class TileManager : MonoBehaviour {

	private Vector2 position;
	public GameObject contains;

 
    public void SetPosition(int x, int y)
	{
		position.x = x;
		position.y = y;
	}

	public Vector2 GetPosition()
	{
		return position;
	}

	public void SetContains(GameObject gameObject)
	{
		contains = gameObject;
	}

	public GameObject GetContains()
	{
		return contains;
	}
}
