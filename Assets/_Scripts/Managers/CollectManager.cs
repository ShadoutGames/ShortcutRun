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
        private GameObject[] throwedWoods;

        private int currentWoodCount = 10;

        #endregion

        #region Props

        public int CurrentWoodCount => currentWoodCount;

        #endregion

        #region Unity Methods

        private void Start()
        {
            woods = new GameObject[maxWoodCount];
            throwedWoods = new GameObject[maxWoodCount];

            for (int i = 0; i < maxWoodCount; i++)
            {
                var wood = Instantiate(woodPrefab, new Vector3(parentWoods.position.x, parentWoods.position.y + i * offsetY, parentWoods.position.z), Quaternion.identity, parentWoods);
                var throwed = Instantiate(woodPrefab, new Vector3(parentWoods.position.x, parentWoods.position.y + i * offsetY, parentWoods.position.z), Quaternion.identity);
                woods[i] = wood;
                throwedWoods[i] = throwed;
                throwed.SetActive(false);
                wood.SetActive(false);

            }

            for (int i = 0; i < currentWoodCount; i++)
            {
                woods[i].SetActive(true);
            }

        }


        private void Update()
        {
            if (currentWoodCount == 1 && currentWoodCount == 0) return;

            for (int i = 1; i < currentWoodCount; i++)
            {
                woods[i].transform.localPosition = Vector3.Lerp(woods[i].transform.localPosition, woods[i - 1].transform.localPosition + offsetY * Vector3.up, (currentWoodCount - i) * .03f);
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
            woods[currentWoodCount - 1].SetActive(false);
            currentWoodCount--;
        }
        
        public void WoodAling()
        {
            if (currentWoodCount > 0)
            {
                throwedWoods[currentWoodCount - 1].transform.position = new Vector3(Player.Instance.transform.position.x,
                0,
                Player.Instance.transform.position.z);
                throwedWoods[currentWoodCount - 1].transform.rotation = Player.Instance.transform.rotation;
                throwedWoods[currentWoodCount - 1].SetActive(true);
                Remove();
            }
            // else if (Player.Instance.State != PlayerState.Fly)
            // {
            //     Player.Instance.ChangePlayerState(PlayerState.Fly);
            //     Player.Instance.Jump();
            //     if (Player.Instance.transform.position.y == 0)
            //     {
            //         Player.Instance.gameObject.GetComponent<Rigidbody>().useGravity = false;
            //     }

            // }
            else if (currentWoodCount == 0)
            {
                GameManager.Instance.ChangeGameState(GameState.Lose);
                Player.Instance.rb.isKinematic = true;
                
            }
            

        }

        #endregion

        #region Callbacks
        #endregion
    }
}
