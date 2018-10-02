using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scr_InputManager {

	public static bool disableInput = false; //set to true to prevent the player from getting input

	// -- Axis
	/// <summary>
	/// Xbox one - L stick / D-pad
	/// Keyboard - AD
	/// </summary>
	/// <returns>returns -1 for left, 1 for right, 0 for neither, only on the frame the axis is pressed</returns>
	public static int MainHorizontal()
	{
		if(disableInput)
		{
			return 0;
		}

		float r = 0.0f;
		r += Input.GetAxis("J_MainHorizontal");
		r += Input.GetAxis("J_SecondHorizontal");
		r += Input.GetAxis("K_MainHorizontal");
		/*
		if (!Input.GetButtonDown("J_MainHorizontal")){
			Debug.Log("GetButtonDown returned false");
		 	return 0;
		}
		 */
		if (r < 0f)
		{
			return -1;
		}
		else if (r > 0f)
		{
			return 1;
		}
		return 0;
	}

	/// <summary>
	/// Xbox one - L stick / D-pad
	/// Keyboard - WS
	/// </summary>
	/// <returns>returns -1 for down, 1 for up, 0 for neither</returns>
	public static int MainVertical()
	{
		if(disableInput)
		{
			return 0;
		}

		float r = 0.0f;
		r += Input.GetAxis("J_MainVertical");
		r += Input.GetAxis("J_SecondVertical");
		r += Input.GetAxis("K_MainVertical");
		if (r < 0f)
		{
			return 1;
		}
		else if (r > 0f)
		{
			return -1;
		}
		return 0;
	}
	/// <summary>
	/// Xbox one - R stick / LT / RT
	/// Keyboard - Mouse Wheel up/down
	/// </summary>
	/// <returns>returns -1 for left, 1 for right, 0 for neither</returns>
	public static int HandScrolling()
	{
		if(disableInput)
		{
			return 0;
		}

		float r = 0.0f;
		r += Input.GetAxis("J_HandScroll");
		r += Input.GetAxis("Mouse ScrollWheel");
		if (r < 0f)
		{
			return -1;
		}
		else if (r > 0f)
		{
			return 1;
		}
		return 0;
	}

	// -- Buttons

	/// <summary>
	/// Xbox one - Y button
	/// Keyboard - 1/2/3
	/// </summary>
	/// <returns>returns true if any of these buttons were pressed this frame</returns>
	public static bool SoulFusion()
	{
		if(disableInput)
		{
			return false;
		}

		return Input.GetButtonDown("Y_Button") || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);
	}

	/// <summary>
	/// Used with keyboard controls.
	/// </summary>
	/// <returns>returns either 1,2, or 3 to indicate a selection, or 0 if none was selected</returns>
	public static int K_SoulFusion()
	{
		if(disableInput)
		{
			return 0;
		}

		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
		{
			return 1;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
		{
			return 2;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
		{
			return 3;
		}

		return 0;
	}

	/// <summary>
	/// XBox one - B button
	/// Keyboard - LMB
	/// </summary>
	/// <returns>returns true if the blast button is down.</returns>
	public static bool Blast_Down()
	{
		if(disableInput)
		{
			return false;
		}

		return Input.GetButtonDown("Blast_Button");
	}

	public static bool Blast_Up()
	{
		if(disableInput)
		{
			return false;
		}

		return Input.GetButtonUp("Blast_Button");
	}

	/// <summary>
	/// Xbox one - A button
	/// Keyboard - RMB
	/// </summary>
	/// <returns>returns true the frame the play card button is pressed</returns>
	public static bool PlayCard()
	{
		if(disableInput)
		{
			return false;
		}
		return Input.GetButtonDown("PlayCard_Button");
	}

/* 
	/// <summary>
	/// Xbox one - ?
	/// Keyboard - Space Bar
	/// </summary>
	/// <returns></returns>
	public static bool SoulTap()
	{

	}
*/
}
