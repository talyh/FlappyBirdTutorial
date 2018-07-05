// "using" are C#'s version of JS "import"
// They may be entire packages or just namespaces (like importing a React Component to be used somewhere else)
// System and its derivates are C# standard, while UnityEngine ones allows us to access things specific to Unity
// Classes in the same namespace (or where no namespace is defined) don't need to be cross-imported 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	// Think of this as a React's component props
	private Rigidbody2D _rigidbody;

	// this is called by the engine when the game scene is loaded
	private void Start()
	{
		// get and store a reference to the Rigidbody2D attached to this GameObject
		_rigidbody = GetComponent<Rigidbody2D>();

		// start the object moving in the x-axis, with the scrollSpeed determined in the GameController
		_rigidbody.velocity = GameController.instance.scrollSpeed;
	}

	// this is called by the engine on every frame of the game
	private void Update()
	{
		// if the game is over, stop scrolling
		if (GameController.instance.gameOver)
		{
			_rigidbody.velocity = Vector2.zero;
		}
	}
}