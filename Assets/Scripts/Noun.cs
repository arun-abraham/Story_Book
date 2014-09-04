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
			objectDefaultVerbs.Add(objectVerbs[i]);
		}
	}

    public Verb FindObjectConnection(Noun obj)
    {
        Verb connection = null;
        for (int i = 0; i < objectVerbs.Count && connection == null; i++)
        {
            if (objectVerbs[i].obj = obj)
            {
                connection = objectVerbs[i].verb;
            }
        }
        return connection;
    }

    [System.Serializable]
	public class ObjectVerb
	{
		public Noun obj;
		public Verb verb;
	}
}
