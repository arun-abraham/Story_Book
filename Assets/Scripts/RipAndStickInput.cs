using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RipAndStickInput : MonoBehaviour {
	public TextMesh storyText;
	public Noun person;
	public Noun wall;
	public Inventory inventory;
	public Vector3 personPos;
	public Vector3 wallPos;

	void Start()
	{
		UpdateStory();
	}

	void Update()
	{
		// Remove or place wall.
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (inventory.RemoveNoun(wall))
				{
					wall.transform.position = wallPos;
				}
			}
			else
			{
				Vector3 tempPos = wall.transform.position;
				if (inventory.AddNoun(wall))
				{
					wallPos = tempPos;
				}
			}

			UpdateStory();
		}

		// Remove or place person.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (inventory.RemoveNoun(person))
				{
					person.transform.position = personPos;
				}
			}
			else
			{
				Vector3 tempPos = person.transform.position;
				if (inventory.AddNoun(person)) 
				{
					personPos = tempPos;
				}
			}

			UpdateStory();
		}
	}

	private void UpdateStory()
	{
		//storyText.text = subjects[subjectIndex].id + " " + subjects[subjectIndex].FindObjectConnection(objects[objectIndex]).action + " " + objects[objectIndex].id;
	}
}
