using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageManager : MonoBehaviour {
	private int pageIndex = -1;
	public int PageIndex
	{
		get { return pageIndex; }
	}
	public int startPage = 0;
	public TextMesh pageNumberText;
	public List<Page> pages;
	public List<Noun> allNouns = new List<Noun>();

	void Start()
	{
		// Set nouns' first pages based on when they appear in noun placements.
		for (int i = 0; i < pages.Count; i++)
		{
			for (int j = 0; j < pages[i].nounPlacements.Count; j++)
			{
				Noun noun = pages[i].nounPlacements[j].noun;
				if (!noun.inInventory && (noun.firstPage < 0 || noun.firstPage > i))
				{
					noun.firstPage = i;
				}
			}
		}
	}

	void Update()
	{
		if (pageIndex < 0)
		{
			foreach(Noun noun in allNouns)
			{
				noun.HideInPage();
			}
			pageIndex = startPage;
			DisplayPage();
		}
	}

	public void DisplayPage()
	{
		List<NounPlacement> pageNouns = pages[pageIndex].nounPlacements;
		for (int i = 0; i < pageNouns.Count; i++)
		{
			Noun pageNoun = pageNouns[i].noun;
			if (!pageNoun.inInventory && pageIndex >= pageNoun.firstPage)
			{
				pageNoun.DisplayInPage();
				VerbTag.Relationship startActionModifier = VerbTag.Relationship.NONE;
				if (pageNouns[i].startAction != null)
				{
					startActionModifier = pageNouns[i].startAction.modifier;
				}
				Transform controlledTransform = pageNoun.transform;
				if (pageNoun.container != null)
				{
					controlledTransform = pageNoun.container.transform;
				}
				
				bool nounModified = false;
				if (pageIndex > 0)
				{
					NounPlacement placeInPrev = FindNounPlacement(pageIndex - 1, pageNoun);
					if (placeInPrev != null)
					{
						switch (startActionModifier)
						{
							case VerbTag.Relationship.INTERRUPT:
								nounModified = true;
								controlledTransform.position = placeInPrev.startPosition;
								break;
						}
					}
				}
				
				
				if (!nounModified)
				{
					controlledTransform.position = pageNouns[i].startPosition;
				}
			}
		}

		if (pageNumberText != null)
		{
			pageNumberText.text = "" + (pageIndex + 1);
		}
	}

	public void HidePage()
	{
		List<NounPlacement> pageNouns = pages[pageIndex].nounPlacements;
		for (int i = 0; i < pageNouns.Count; i++)
		{
			if (!pageNouns[i].noun.inInventory)
			{
				pageNouns[i].noun.HideInPage();
			}
		}
	}

	public void PreviousPage()
	{
		if (pageIndex > 0)
		{
			HidePage();
			pageIndex--;
			DisplayPage();
		}
	}

	public void NextPage()
	{
		if (pageIndex < pages.Count - 1)
		{
			HidePage();
			pageIndex++;
			DisplayPage();
		}
	}

	public NounPlacement FindNounPlacement(int pageNumber, Noun noun)
	{
		List<NounPlacement> pageNouns = pages[pageNumber].nounPlacements;
		NounPlacement nounPlacement = null;
		for (int i = 0; i < pageNouns.Count && nounPlacement == null; i++)
		{
			if (pageNouns[i].noun == noun)
			{
				nounPlacement = pageNouns[i];
			}
		}
		return nounPlacement;
	}
}

[System.Serializable]
public class Page
{
	public List<NounPlacement> nounPlacements;
	bool sharePrevSetting;
}

[System.Serializable]
public class NounPlacement
{
	public Noun noun;
	public Vector3 startPosition;
	public ObjectVerb startAction;
}