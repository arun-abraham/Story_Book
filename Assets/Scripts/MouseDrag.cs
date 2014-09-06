using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {

	private GameObject sprite;
	private Vector3 spriteCoordinates;
	private Vector3 mouseCoordinates;
	private Vector3 spriteToMouse; 
	//private Ray ray;
	private RaycastHit hit;
	private bool spriteSelected;
	private Vector3 temp;
	public string grabLayer;

	// Use this for initialization
	void Start () {
		hit = new RaycastHit ();
		spriteSelected = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0))
		{
			//spriteCoordinates = Camera.main.WorldToScreenPoint (sprite.transform.position);

			//check if mouse is grabbing the sprite
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask(new string[] {grabLayer})))
				if (hit.transform != null)
				{
					sprite = hit.transform.gameObject;
					spriteSelected = true;
					//Get the current pixel coords of mouse
					mouseCoordinates = Input.mousePosition;
					//find the current pixel coords of sprite
					spriteCoordinates = Camera.main.WorldToScreenPoint(sprite.transform.position);
					//calculate relative distance
					spriteToMouse = mouseCoordinates - spriteCoordinates;
					//save the z value of sprite
					temp.z = sprite.transform.position.z;
				}				
		}
		if (Input.GetMouseButton(0))
		{
			// Camera.main.ScreenPointToRay(Input.mousePosition);
			if (spriteSelected && sprite != null)
			{
				{
					mouseCoordinates = Input.mousePosition;
					spriteCoordinates = mouseCoordinates - spriteToMouse;
					sprite.transform.position = Camera.main.ScreenToWorldPoint (spriteCoordinates);
					//reset sprite to its original z value
					temp.x = sprite.transform.position.x;
					temp.y = sprite.transform.position.y;
					sprite.transform.position = temp;
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			spriteSelected = false;
			sprite = null;
		}
	}
}
