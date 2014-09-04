using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Noun : MonoBehaviour {
	public string id;
	public bool possibleSubject;
	public List<ObjectVerb> objectVerbs;
	[HideInInspector]
	public List<ObjectVerb> objectDefaultVerbs;

	void Start () 
	{
		if (objectVerbs == null)
		{
			objectVerbs = new List<ObjectVerb>();
		}
		objectDefaultVerbs = new List<ObjectVerb>();
		ResetDefaultVerbsToCurrent();
	}

	void Update () 
	{
		
	}

	private void ResetDefaultVerbsToCurrent() 
	{
		for (int i = 0; i < objectVerbs.Count; i++) 
		{
			if (i >= objectDefaultVerbs.Count)
			{
				objectDefaultVerbs.Add(new ObjectVerb(objectVerbs[i]));
			} 
			else
			{
				objectDefaultVerbs[i] = new ObjectVerb(objectVerbs[i]);
			}
			
		}
	}

	private void ResetVerbsToDefault()
	{
		objectVerbs.Clear();
		for (int i = 0; i < objectDefaultVerbs.Count; i++)
		{
			if (i >= objectVerbs.Count)
			{
				objectVerbs.Add(new ObjectVerb(objectDefaultVerbs[i]));
			}
			else
			{
				objectVerbs[i] = new ObjectVerb(objectDefaultVerbs[i]);
			}
		}
	}

	public Verb FindObjectConnection(Noun obj)
	{
		Verb connection = null;
		for (int i = 0; i < objectVerbs.Count && connection == null; i++)
		{
			if (objectVerbs[i].obj == obj)
			{
				connection = objectVerbs[i].verb;
			}
		}
		return connection;
	}

	public Verb FindObjectDefaultConnection(Noun obj)
	{
		Verb connection = null;
		for (int i = 0; i < objectDefaultVerbs.Count && connection == null; i++)
		{
			if (objectDefaultVerbs[i].obj == obj)
			{
				connection = objectDefaultVerbs[i].verb;
			}
		}
		return connection;
	}

	public Verb SetObjectConnection(Noun obj, Verb connection)
	{
		bool setConnection = false;
		for (int i = 0; i < objectVerbs.Count && !setConnection; i++)
		{
			if (objectVerbs[i].obj == obj)
			{
				objectVerbs[i].verb = connection;
				setConnection = true;
			}
		}
		return connection;
	}

	[System.Serializable]
	public class ObjectVerb
	{
		public Noun obj;
		public Verb verb;

		public ObjectVerb(Noun obj, Verb verb)
		{
			this.obj = obj;
			this.verb = verb;
		}

		public ObjectVerb(ObjectVerb original)
		{
			this.obj = original.obj;
			this.verb = original.verb;
		}
	}
}
