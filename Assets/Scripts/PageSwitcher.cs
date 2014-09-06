using UnityEngine;
using System.Collections;

public class PageSwitcher : MonoBehaviour {
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			SendMessage("PreviousPage", SendMessageOptions.DontRequireReceiver);
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			SendMessage("NextPage", SendMessageOptions.DontRequireReceiver);
		}
	}
}
