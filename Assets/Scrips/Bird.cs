// "using" are C#'s version of JS "import"
// They may be entire packages or just namespaces (like importing a React Component to be used somewhere else)
// System and its derivates are C# standard, while UnityEngine ones allows us to access things specific to Unity
// Classes in the same namespace (or where no namespace is defined) don't need to be cross-imported 
using System;
using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
	// Think of these as a React's component props
	// The "SerializedField" ones have the value set by a Game Designer in the engine (or the initialization value if nothing was provided)
	// The others, are determined through code, either as a variable initialization, or as the game progresses
	[SerializeField]
	private float _flapForce = 200; // upward force of the "flap"

	private Animator _animator = null; // reference to the Animator component
	private Rigidbody2D _rigidbody = null; // reference to the Rigidbody2D component

	// this is called by the engine when the game scene is loaded
	private void Start()
	{
		// get and store a reference to the Animator component attached to this GameObject
		_animator = GetComponent<Animator>();

		// get and store a reference to the Rigidbody2D component attached to this GameObject
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	// this is called by the engine on every frame of the game
	private void Update()
	{
		// if the bird is dead, don't bother checking anything else
		if (GameController.instance.gameOver)
		{
			return;
		}

		// look for input to trigger a "flap".
		// this is derived from the engine, and listens for input, in this case, a mouse click or tap event
		if (Input.GetMouseButtonDown(0))
		{
			FlapOnClick();
		}
	}

	// do the actual flapping action, benefitting fro \m the engine's access to animation and rigidbody properties
	private void FlapOnClick()
	{
		// tell the animator about it so it plays the Flap animation
		_animator.SetTrigger("Flap");

		// zero out the birds current y velocity so it doesn't compound with the new flap
		_rigidbody.velocity = Vector2.zero;

		// give the bird no force in the x axis, and flapForce in y
		// so it moves up but stays in the same horizontal position
		_rigidbody.AddForce(new Vector2(0, _flapForce));
	}

	// this is called whenever the engine detects this gameObject collided with another
	// it needs the Collision2D parameter so it can access the properties of whatever it collided with
	void OnCollisionEnter2D(Collision2D other)
	{
		// if colliding with the ceiling, don't do anything
		if (other.gameObject.CompareTag("Ceiling"))
		{
			return;
		}

		// tell the animator about it so it plays the Die animation
		_animator.SetTrigger("Die");

		// zero out the bird's velocity, so it doesn't move based on remaining forces
		_rigidbody.velocity = Vector2.zero;

		// notify the GameController
		GameController.instance.GameOver();
	}

	// this is called whenever the engine detects this gameObject entered another object's trigger area
	// it needs the Collider2D parameter so it can access the properties of whatever was triggered
	void OnTriggerEnter2D(Collider2D other)
	{
		// if the trigger was a column gap, score points
		if (other.gameObject.CompareTag("Column"))
		{
			GameController.instance.Score();
		}
	}
}