using System;
using NaughtyAttributes;
using NoName.Utilities;
using UnityEngine;

namespace NoName
{
public class CollectManager : Singleton<CollectManager>
{
	#region SerializedFields

	[SerializeField]
	private GameObject woodPrefab;

	[SerializeField]
	private int maxWoodCount;

	[SerializeField]
	private Transform parentWoods;

	[SerializeField]
	private float offsetY;

	#endregion

	#region Variables
	
	private GameObject[] woods;

	private int currentWoodCount = 0;


	#endregion

	#region Props

	public int CurrentWoodCount => currentWoodCount;

	#endregion

	#region Unity Methods

	private void Start()
	{
		woods = new GameObject[maxWoodCount];

		for (int i = 0; i < maxWoodCount; i++)
		{
			var wood = Instantiate(woodPrefab, new Vector3(parentWoods.position.x, parentWoods.position.y + i * offsetY, parentWoods.position.z), Quaternion.identity, parentWoods);
			woods[i] = wood;
			wood.SetActive(false);
		}

		for (int i = 0; i < currentWoodCount; i++)
		{
			woods[i].SetActive(true);
		}
	}

	private void Update() 
	{
		if(currentWoodCount == 1 && currentWoodCount == 0) return;

		for (int i = 1; i < currentWoodCount; i++)
		{
			woods[i].transform.localPosition = Vector3.Lerp(woods[i].transform.localPosition, woods[i - 1].transform.localPosition + offsetY * Vector3.up, (currentWoodCount - i) *.03f);
		}
	}

	#endregion

	#region Methods
	
	public void Add()
    {
        woods[currentWoodCount].SetActive(true);
		currentWoodCount++;
    }
	
    public void Remove()
    {
        woods[currentWoodCount-1].SetActive(false);
		currentWoodCount--;
    }
    
    #endregion

	#region Callbacks

	#endregion
}
}