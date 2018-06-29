using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public float upForce = 200; //Upward force of the "flap".
	private bool _isDead = false; //Has the player collided with a wall?

	private Animator _animator; //Reference to the Animator component.
	private Rigidbody2D _rigidbody; //Holds a reference to the Rigidbody2D component of the bird.

	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		_animator = GetComponent<Animator>();

		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Don't allow control if the bird has died.
		if (_isDead == false)
		{
			//Look for input to trigger a "flap".
			if (Input.GetMouseButtonDown(0))
			{
				//...tell the animator about it and then...
				_animator.SetTrigger("Flap");

				//...zero out the birds current y velocity before...
				_rigidbody.velocity = Vector2.zero;

				//  new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.
				_rigidbody.AddForce(new Vector2(0, upForce));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ceiling"))
		{
			return;
		}

		// Zero out the bird's velocity
		_rigidbody.velocity = Vector2.zero;

		// If the bird collides with something set it to dead...
		_isDead = true;

		//...tell the Animator about it...
		_animator.SetTrigger("Die");

		//...and tell the game control about it.
		GameController.instance.BirdDied();
	}
}