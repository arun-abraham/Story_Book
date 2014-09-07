using UnityEngine;
using System.Collections;

public class NarrativeManager : MonoBehaviour {
	private static NarrativeManager instance = null;
	public static NarrativeManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("Globals").GetComponent<NarrativeManager>();
			}
			return instance;
		}
	}
	public Narrative nextDialog;
	public TextMesh dialogText;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PlayDialog();
		}
	}

	public void PlayDialog()
	{
		if (nextDialog != null)
		{
			dialogText.text = nextDialog.narText;
			nextDialog = nextDialog.nextNar;
		}
		else
		{
			dialogText.text = null;
		}
	}
}
