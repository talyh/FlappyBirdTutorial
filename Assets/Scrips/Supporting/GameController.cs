// "using" are C#'s version of JS "import"
// They may be entire packages or just namespaces (like importing a React Component to be used somewhere else)
// System and its derivates are C# standard, while UnityEngine ones allows us to access things specific to Unity
// Classes in the same namespace (or where no namespace is defined) don't need to be cross-imported 
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// Think of these as a React's component props
	// The "SerializedField" ones have the value set by a Game Designer in the engine (or the initialization value if nothing was provided)
	// The others, are determined through code, either as a variable initialization, or as the game progresses
	[SerializeField]
	private Text _scoreText; // reference to the UI text component that displays the player's score.
	[SerializeField]
	private GameObject _gameOverMessage; // reference to the object that displays the text which appears when the player dies.

	private int _score = 0; // the player's score.
	public bool gameOver; // control whether the game is over
	public Vector2 scrollSpeed = new Vector2(-1.5f, 0); // the speed at which all objects should scroll
	public static GameController instance; // reference to our GameController script so we can access it statically

	// this is called by the engine when the game scene is loaded
	private void Awake()
	{
		// if we don't currently have a GameController
		if (!instance)
		{
			// set this one to be it
			instance = this;
		}
		// if the instance is not the version of GameController that's awakening
		else
		{
			// destroy this one because it is a duplicate.
			Destroy(gameObject);
		}
	}

	// this is called by the engine on every frame of the game
	private void Update()
	{
		// if the game is over and the player has flapped again
		if (gameOver && Input.GetMouseButtonDown(0))
		{
			// use Unity's built-in library to reload the current scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void Score()
	{
		// can't score if the game is over
		if (gameOver)
		{
			return;
		}

		// If the game is not over, increase the score
		_score++;

		// and adjust the score text
		_scoreText.text = "Score: " + _score.ToString();
	}

	public void GameOver()
	{
		// activate the game over message
		_gameOverMessage.SetActive(true);
		// set the game to be over
		gameOver = true;
	}
}