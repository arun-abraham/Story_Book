using UnityEngine;
using System.Collections;

public class PageSwitcher : MonoBehaviour {
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			SendMessage("PreviousPage", SendMessageOptions.DontRequireReceiver);
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			SendMessage("NextPage", SendMessageOptions.DontRequireReceiver);
		}
	}
}
