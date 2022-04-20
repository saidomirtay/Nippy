using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	AudioManager audioManager;
	[SerializeField] private List<GameObject> objects = new List<GameObject>();
	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject food;
	[SerializeField] private GameObject enemySkeleton;
	public bool isGameActive;
	private float acceleration = 0.4f;
	private float spawnRate;

	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}
	private Vector3 PositionGenerator()
	{
		Vector3 position = new Vector3(Random.Range(-7, 7), Random.Range(-3.5f, 3.5f), 0);
		return position;
	}

	private Quaternion RotationGenerator()
	{
		Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		return rotation;
	}

	private void SpawnObjects()
	{
		int probability = Random.Range(0, 5);
		if (objects.Count > 0 && probability >= 3)
		{
			int index = Random.Range(0, objects.Count);
			Vector3 obstaclePosition = PositionGenerator();
			Quaternion obstacleRotation = RotationGenerator();
			Instantiate(objects[index], obstaclePosition, obstacleRotation);
		}
		for (int i = 0; i < 2; i++)
		{
			Vector3 enemyPosition = PositionGenerator();
			Quaternion enemyRotation = RotationGenerator();
			StartCoroutine(EnemySpawning(enemyPosition, enemyRotation));

			Vector3 foodPosition = PositionGenerator();
			Instantiate(food, foodPosition, Quaternion.Euler(0, 0, 0));
		}
		audioManager.Play("spawn");

	}

	private IEnumerator EnemySpawning(Vector3 position, Quaternion rotation)
	{
		GameObject current = Instantiate(enemySkeleton, position, rotation) as GameObject;
		yield return new WaitForSeconds(1f);
		Destroy(current);
		Instantiate(enemy, position, rotation);

	}

	private IEnumerator SpawnCoroutine()
	{
		if (isGameActive)
			SpawnObjects();
		yield return new WaitForSeconds(spawnRate);
		if (spawnRate > 0f)
		{
			spawnRate -= acceleration;
			acceleration /= 1.2f;
		}
		StartCoroutine("SpawnCoroutine");
	}
	private void Start()
	{
		isGameActive = true;
		spawnRate = 4.5f;
		StartCoroutine("SpawnCoroutine");
	}
}
