using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	public float itemSpacing;
	public List<Noun> nouns;

	public bool AddNoun(Noun noun)
	{
		bool alreadyIn = false;
		for (int i = 0; i < nouns.Count && !alreadyIn; i++)
		{
			if (nouns[i] == noun)
			{
				alreadyIn = true;
			}
		}
		if (alreadyIn)
		{
			return false;
		}

		Vector3 scale = noun.transform.localScale;
		noun.transform.parent = transform;
		noun.transform.localScale = scale;
		noun.transform.localPosition = new Vector3(itemSpacing * nouns.Count, 0, 0);
		nouns.Add(noun);

		if (noun.outline != null)
		{
			noun.outline.SetActive(false);
		}
		
		return true;
	}

	public bool RemoveNoun(Noun noun)
	{
		int foundIndex = -1;
		for (int i = 0; i < nouns.Count && foundIndex < 0; i++)
		{
			if (nouns[i] == noun)
			{
				foundIndex = i;
			}
		}
		if (foundIndex < 0)
		{
			return false;
		}

		nouns.RemoveAt(foundIndex);
		Vector3 scale = noun.transform.localScale;
		noun.transform.parent = null;
		noun.transform.localScale = scale;
	
		if (noun.outline != null)
		{
			noun.outline.SetActive(true);
		}

		for (int i = foundIndex; i < nouns.Count; i++)
		{
			noun.transform.localPosition = new Vector3(itemSpacing * i, 0, 0);
		}

		return true;
	}
}
