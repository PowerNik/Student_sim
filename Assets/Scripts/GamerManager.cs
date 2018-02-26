using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerManager : MonoBehaviour {

	[SerializeField]
	private GameObject personPrefab;

	[SerializeField]
	private Vector3[] spawnPositions;

	[Space(10)]
	[SerializeField]
	private Transform[] goals;

	void Awake ()
	{
		CreatePersons();
	}
	
	private void CreatePersons()
	{
		GameObject personContainer = new GameObject("PersonContainer");
		foreach (Vector3 position in spawnPositions)
		{
			GameObject personGO = Instantiate(personPrefab, position, Quaternion.identity);
			personGO.transform.parent = personContainer.transform;

			Person person = personGO.GetComponent<Person>();
			person.needGoal += OnPersonNeedGoal;
		}
	}

	private void OnPersonNeedGoal(Person person)
	{
		int index = Random.Range(0, goals.Length);
		person.SetGoal(goals[index]);
	}
}
