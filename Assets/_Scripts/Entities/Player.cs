using System;
using System.Collections.ObjectModel;
using NoName.Utilities;
using UnityEngine;

namespace NoName
{
    public class Player : Singleton<Player>
    {
        #region SerializedFields

        [SerializeField]
        private float speed;

        [SerializeField]
        private float turnSpeed;
        [SerializeField]
        private int jumpForce;

        #endregion

        #region Variables

        private Rigidbody rb;
        private Transform _transform;
        private float distanceTravelled;
        private float currentSpeed;
        private float timer = 0;

        #endregion

        #region Props
        public PlayerState State { get; private set; }

        #endregion
        #region Actions
        public static event Action<PlayerState> PlayerStateChanged;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Start()
        {
            ChangePlayerState(PlayerState.Wait);
            InputSystem.Instance.TouchPositionChanged += OnTouchPositionChanged;
            GameManager.AfterStateChanged += OnAfterStateChanged;

        }

        private void Update()
        {
            timer -= Time.deltaTime;
            rb.velocity = _transform.forward * currentSpeed;

            var closestDistance = PathManager.Instance.Path.path.GetClosestPointOnPath(transform.position);
            if (Vector3.Distance(transform.position, closestDistance) > PathManager.Instance.PathMesh.roadWidth)
            {
                if (timer <= 0)
                {
                    CollectManager.Instance.WoodAling();
                    timer = .1f;
                }
                Debug.Log("asd");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var collectWood = other.GetComponent<ICollectable>();
            if (collectWood != null)
            {
                CollectManager.Instance.Add();
                other.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Methods

        public void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce);
            rb.useGravity = true;
        }
        public void ChangePlayerState(PlayerState newState)
        {
            State = newState;
            switch (State)
            {
                case PlayerState.Wait:
                    break;
                case PlayerState.Run:
                    break;
                case PlayerState.Fly:
                    break;
                case PlayerState.Dance:
                    break;
            }
            PlayerStateChanged?.Invoke(State);
        }

        #endregion

        #region Callbacks

        private void OnTouchPositionChanged(Touch touch)
        {
            GameManager.Instance.ChangeGameState(GameState.InGame);
            _transform.Rotate(Vector3.up * touch.deltaPosition.x * turnSpeed);
            Debug.Log("touch");
        }

        private void OnAfterStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Starting:
                    currentSpeed = 0;
                    break;
                case GameState.InGame:
                    gameObject.GetComponent<Animator>().SetTrigger("OnRun");
                    currentSpeed = speed;
                    break;
                case GameState.Win:
                    break;
                case GameState.Lose:
                    break;
            }

        }


        #endregion
    }
}
