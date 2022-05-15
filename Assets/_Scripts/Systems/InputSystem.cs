using System;
using NoName.Utilities;
using UnityEngine;

public class InputController : Singleton<InputController>
{

	#region Actions

	public event Action<Touch> TouchPositionChanged;

	#endregion

	#region Unity Methods

	private void Update() 
	{
		if(Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			TouchPositionChanged?.Invoke(touch);
		}
	}

	#endregion
}