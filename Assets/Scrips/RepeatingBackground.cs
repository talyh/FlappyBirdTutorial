// "using" are C#'s version of JS "import"
// They may be entire packages or just namespaces (like importing a React Component to be used somewhere else)
// System and its derivates are C# standard, while UnityEngine ones allows us to access things specific to Unity
// Classes in the same namespace (or where no namespace is defined) don't need to be cross-imported 
using System.Collections;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

	// Think of these as a React's component props
	private BoxCollider2D _boxCollider; // reference to the Collider attached to this GameObject
	private float _horizontalLength; // the x-axis length of the collider attached to this GameObject

	// this is called by the engine when the game scene is loaded
	private void Awake()
	{
		// get and store a reference to the Collider2D attached to Ground
		_boxCollider = GetComponent<BoxCollider2D>();

		// store the size of the collider along the x axis (its length in units).
		_horizontalLength = _boxCollider.size.x;
	}

	// this is called by the engine on every frame of the game

	private void Update()
	{
		// because the Game Camera controlled by the engine starts at 0, if this object's current x value
		// is smaller then the size of the collider, it was scrolled out of view
		// if this was scrolled out of view
		if (transform.position.x < -_horizontalLength)
		{
			// reposition it so it can be reused
			RepositionBackground();
		}
	}

	// moves the object this script is attached to right in order to create a looping background effect
	private void RepositionBackground()
	{
		// determine how much to move the background object by
		// we're going with twice its length, positioning it directly to the right of the currently visible background object
		Vector2 offset = new Vector2(_horizontalLength * 2f, 0);

		// move the object from it's current position to the one we just calculated
		transform.position = (Vector2) transform.position + offset;
	}
}