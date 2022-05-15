using System;
using UnityEngine;

namespace NoName
{
public class CollectManager : MonoBehaviour
{
	#region SerializedFields
	[SerializeField]
	private GameObject woodPrefab;

	[SerializeField]
	private int maxWoodCount;

	#endregion

	#region Variables
	
	private GameObject[] woods;

	private int currentWoodCount;


	#endregion

	#region Props

	#endregion

	#region Unity Methods

	private void Start()
	{
		woods = new GameObject[maxWoodCount];

		for (int i = 0; i < maxWoodCount; i++)
		{
			var wood = Instantiate(woodPrefab, Player.Instance.gameObject.transform.position, Quaternion.identity,Player.Instance.transform);
			woods[i] = wood;
			wood.transform.position +=new Vector3(transform.position.x,transform.position.y+i,transform.position.z);
			wood.SetActive(true);
		}
	}

	#endregion

	#region Methods

	#endregion

	#region Callbacks

	#endregion
}
}