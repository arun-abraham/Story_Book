using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Verb : MonoBehaviour {
	public string action;
	public List<VerbTag> tags;
}

[System.Serializable]
public class VerbTag
{
	public enum Relationship
	{
		DEPENDS = 0,
		INTERRUPTS
	}

	public string tagName;
	public Relationship relationship;
}