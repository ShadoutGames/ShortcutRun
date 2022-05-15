using System;
using NoName.Utilities;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{

	#region Actions

	public event Action<Touch> TouchPositionChanged;

	#endregion

	#region Unity Methods

	private void FixedUpdate() 
	{
		if(Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			TouchPositionChanged?.Invoke(touch);
		}
	}

	#endregion
}