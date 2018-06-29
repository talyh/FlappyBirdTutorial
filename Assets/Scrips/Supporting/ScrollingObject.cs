using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		_rigidbody = GetComponent<Rigidbody2D>();

		//Start the object moving.
		_rigidbody.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
	}

	void Update()
	{
		// If the game is over, stop scrolling.
		if (GameController.instance.gameOver == true)
		{
			_rigidbody.velocity = Vector2.zero;
		}
	}
}