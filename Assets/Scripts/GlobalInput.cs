using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalInput : MonoBehaviour {
	public TextMesh storyText;
	public List<Noun> subjects;
	public int subjectIndex = 0;
	public List<Noun> objects;
	public int objectIndex = 0;

	void Start()
	{
		UpdateStory();
	}

	void Update()
	{
		// Increment target object.
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Verb connection = subjects[subjectIndex].FindObjectConnection(objects[objectIndex]);
			objectIndex = (objectIndex + 1) % objects.Count;
			if (objects[objectIndex] == subjects[subjectIndex])
			{
				objectIndex = (objectIndex + 1) % objects.Count;
			}

			// If the object is not being replaced, change the verb action to default of new subject-object pair.
			if (!Input.GetKey(KeyCode.LeftShift))
			{
				connection = subjects[subjectIndex].FindObjectDefaultConnection(objects[objectIndex]);
			}

			subjects[subjectIndex].SetObjectConnection(objects[objectIndex], connection);
			
			UpdateStory();
		}

		// Increment target subject.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Verb connection = subjects[subjectIndex].FindObjectConnection(objects[objectIndex]);
			subjectIndex = (subjectIndex + 1) % subjects.Count;
			if (subjects[subjectIndex] == objects[objectIndex])
			{
				subjectIndex = (subjectIndex + 1) % subjects.Count;
			}

			// If the subject is not being replaced, change the verb action to default of new subject-object pair.
			if (!Input.GetKey(KeyCode.LeftShift))
			{
				connection = subjects[subjectIndex].FindObjectDefaultConnection(objects[objectIndex]);
			}

			subjects[subjectIndex].SetObjectConnection(objects[objectIndex], connection);

			UpdateStory();
		}
	}

	private void UpdateStory()
	{
		storyText.text = subjects[subjectIndex].id + " " + subjects[subjectIndex].FindObjectConnection(objects[objectIndex]).action + " " + objects[objectIndex].id;
	}
}
