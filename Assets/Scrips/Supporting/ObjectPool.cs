// "using" are C#'s version of JS "import"
// They may be entire packages or just namespaces (like importing a React Component to be used somewhere else)
// System and its derivates are C# standard, while UnityEngine ones allows us to access things specific to Unity
// Classes in the same namespace (or where no namespace is defined) don't need to be cross-imported 
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
	// Think of these as a React's component props
	// The "SerializedField" ones have the value set by a Game Designer in the engine (or the initialization value if nothing was provided)
	// The others, are determined through code, either as a variable initialization, or as the game progresses
	[SerializeField]
	private GameObject _prefab; // the object to be used as template for the ones we'll have in the pool
	[SerializeField]
	private int _poolSize = 5; // how many objects to keep on standby
	[SerializeField]
	private float _spawnRate = 4; // how quickly they spawn/reposition
	[SerializeField]
	private float _minY = -2; // minimum y value of the new repositioning
	[SerializeField]
	private float _maxY = 2; //Maximum y value of the repositioning
	[SerializeField]
	private Vector2 _objectPoolPosition = new Vector2(-15, -25); // a holding position offscreen for our unused objects

	private GameObject[] _objects; // collection of pooled objects
	private int _currentObject = 0; // index of the current column in the collection
	private float _spawnXPosition = 10f; // where in front of the player should the object be spawned
	private float _timeSinceLastSpawned = 0; // a timer to help us control when it's time to reposition an object

	// this is called by the engine when the game scene is loaded
	private void Start()
	{
		// initialize the collection
		_objects = new GameObject[_poolSize];

		// create a copy of the template in each position of the array
		for (int i = 0; i < _poolSize; i++)
		{
			_objects[i] = Instantiate(_prefab, _objectPoolPosition, Quaternion.identity);
		}
	}

	// this is called by the engine on every frame of the game
	private void Update()
	{
		// increase the timer by the time elapsed since the last frame
		_timeSinceLastSpawned += Time.deltaTime;

		// while the game is not over, whenever the timer the timer reaches the spawn rate
		if (!GameController.instance.gameOver && _timeSinceLastSpawned >= _spawnRate)
		{
			// spawn a new object
			SpawnObject();
		}
	}

	private void SpawnObject()
	{
		// reset the timer
		_timeSinceLastSpawned = 0f;

		// determine a random y position for the object, within boundaries
		float spawnYPosition = Random.Range(_minY, _maxY);

		// set the current object in the array to that position
		// since the player isn't moving, the x position can be fixed, while the random y is used
		_objects[_currentObject].transform.position = new Vector2(_spawnXPosition, spawnYPosition);

		// move the currentObject index to the next position in the array
		_currentObject = (_currentObject + 1) % _poolSize;
	}
}