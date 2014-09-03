using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Noun : MonoBehaviour {
	public bool possibleSubject;
	public List<int> a;
	[SerializeField]
	public Dictionary<Noun, Verb> objectVerbs;
	public List<KeyValuePair<Noun, Verb>> objectDefaultVerbs;

	void Start () 
	{
		if (objectVerbs == null)
		{
			//objectVerbs = new List<KeyValuePair<Noun, Verb>>();
		}
		objectDefaultVerbs = new List<KeyValuePair<Noun, Verb>>();
		ResetDefaultVerbs();
	}

	void Update () 
	{
		
	}

	private void ResetDefaultVerbs() 
	{
		objectDefaultVerbs.Clear();
		for (int i = 0; i < objectVerbs.Count; i++) 
		{
			//objectDefaultVerbs.Add(objectVerbs[i]);
		}
	}

	public class ObjectVerb
	{
		public Noun obj;
		public Verb verb;
	}
}
