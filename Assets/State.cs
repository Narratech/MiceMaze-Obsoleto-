using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	public static State _instance;
	public static State Instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<State>();

				if (_instance == null)
				{
					GameObject container = new GameObject("State");
					_instance = container.AddComponent<State>();
				}
			}

			return _instance;
		}
	}


	private static int m_turn;
	public static int Turn {
		get
		{
			return m_turn;
		}
		set
		{
			m_turn = value;
		}
	}


}
