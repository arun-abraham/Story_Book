using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Verb : MonoBehaviour {
	public string action;
	public string type;
	public List<VerbTag> tags;

	public VerbTag FindRelatedTag(string verbType)
	{
		VerbTag relatedTag = null;
		for (int i = 0; i < tags.Count; i++)
		{
			if (tags[i].tagName == verbType)
			{
				relatedTag = tags[i];
			}
		}
		return relatedTag;
	}
}

[System.Serializable]
public class VerbTag
{
	public enum Relationship
	{
		NONE = 0,
		INTERRUPT
	}

	public string tagName;
	public Relationship relationship;
}