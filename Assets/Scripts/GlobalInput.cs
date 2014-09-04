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
			objectIndex++;
			if (objectIndex >= objects.Count)
			{
				objectIndex = 0;
			}
			if (objects[objectIndex] == subjects[subjectIndex])
			{
				objectIndex++;
			}

			// If the object is not being replaced, change the verb action to default of new object.
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
