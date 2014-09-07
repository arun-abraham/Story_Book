using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageManager : MonoBehaviour {
	private static PageManager instance = null;
	public static PageManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<PageManager>();
			}
			return instance;
		}
	}
	private int pageIndex = -1;
	public int PageIndex
	{
		get { return pageIndex; }
	}
	public int startPage = 1;
	public TextMesh pageNumberText;
	public Inventory inventory;
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
			pageIndex = startPage - 1;
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
				
				
				Transform controlledTransform = pageNoun.transform;
				if (pageNoun.container != null)
				{
					controlledTransform = pageNoun.container.transform;
				}

				VerbTag.Relationship startActionModifier = VerbTag.Relationship.NONE;
				bool nounModified = false;
				if (pageNouns[i].startAction != null)
				{
					startActionModifier = pageNouns[i].startAction.modifier;
					// Determine if dialog text should be shown.
					if (pageNouns[i].startAction.displayDialog)
					{
						// If the action was modified, and the modification has dialog, us that.
						bool displayedModification = false;
						if (pageNouns[i].startAction.modifiedBy != null)
						{
							NounPlacement placeOfModifiedBy = FindNounPlacement(pageIndex - 1, pageNouns[i].startAction.modifiedBy);
							if (placeOfModifiedBy != null && placeOfModifiedBy.startAction != null && placeOfModifiedBy.startAction.eventDialog != null)
							{
								NarrativeManager.Instance.nextDialog = placeOfModifiedBy.startAction.eventDialog;
								placeOfModifiedBy.startAction.eventDialog = null;
								displayedModification = true;
							}
						}
						// If no modification was displayed, use the actions dialog.
						if (!displayedModification && pageNouns[i].startAction.eventDialog != null)
						{
							NarrativeManager.Instance.nextDialog = pageNouns[i].startAction.eventDialog;
							pageNouns[i].startAction.eventDialog = null;
						}
					}
				}
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

				pageNoun.AffectNextPage();
			}
		}

		UpdatePageNumber();

		if (pages[pageIndex].startDialog != null)
		{
			NarrativeManager.Instance.nextDialog = pages[pageIndex].startDialog;
			pages[pageIndex].startDialog = null;
		}
		NarrativeManager.Instance.PlayDialog();
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
		if (pageIndex < pages.Count - 1 && IsProgressable())
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

	public bool IsProgressable()
	{
		bool progressable = true;

		// Prohibit progress if any nouns required for progress are not on the page.
		for (int i = 0; i < inventory.nouns.Count && progressable; i++)
		{
			if (inventory.nouns[i].requiredInPage)
			{
				progressable = false;
			}
		}

		// Prohibit progress if any action that prevents progress is capable of executing.
		for (int i = 0; i < pages[pageIndex].nounPlacements.Count && progressable; i++)
		{
			NounPlacement nounPlacement = pages[pageIndex].nounPlacements[i];
			if (nounPlacement.startAction != null && nounPlacement.startAction.blocksProgress && nounPlacement.noun.InPage)
			{
				if (nounPlacement.startAction.modifier != VerbTag.Relationship.INTERRUPT)
				{
					progressable = false;
				}
			}
		}

		return progressable;
	}

	public bool IsStoryEnd()
	{
		return pageIndex >= pages.Count - 1;
	}

	public void UpdatePageNumber()
	{
		if (pageNumberText != null)
		{
			pageNumberText.text = "" + (pageIndex + 1);
			if (IsStoryEnd())
			{
				pageNumberText.color = Color.yellow;
			}
			else if (IsProgressable())
			{
				pageNumberText.color = Color.white;
			}
			else
			{
				pageNumberText.color = Color.red;
			}
		}
	}
}

[System.Serializable]
public class Page
{
	public List<NounPlacement> nounPlacements;
	//public bool sharePrevSetting; not used yet
	public Narrative startDialog;
}

[System.Serializable]
public class NounPlacement
{
	public Noun noun;
	public Vector3 startPosition;
	public ObjectVerb startAction;
}