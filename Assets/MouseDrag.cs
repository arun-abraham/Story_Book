using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {

	public GameObject sprite;
	public Vector3 spriteCoordinates;
	public Vector3 mouseCoordinates;
	public Vector3 spriteToMouse;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*if (Input.GetMouseButtonDown (0))
		{
				spriteCoordinates = Camera.main.WorldToScreenPoint (sprite.transform.position);
				mouseCoordinates = Input.mousePosition;
		}*/
		if (Input.GetMouseButton(0))
		{
			//spriteCoordinates = Camera.main.WorldToScreenPoint (sprite.transform.position);
			mouseCoordinates = Input.mousePosition;
			spriteCoordinates = mouseCoordinates;
			spriteCoordinates.z = sprite.transform.position.z;
			sprite.transform.position = Camera.main.ScreenToWorldPoint (mouseCoordinates);
			spriteCoordinates.x = sprite.transform.position.x;
			spriteCoordinates.y = sprite.transform.position.y;
			sprite.transform.position = spriteCoordinates;
		}

	}
}
