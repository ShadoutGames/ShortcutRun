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

        #endregion

        #region Variables

        public Rigidbody rb;
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
            var jumpingArea = other.GetComponent<JumpArea>();
            if (jumpingArea != null)
            {
                Jump();
            }
        }

        #endregion

        #region Methods

        public void Jump()
        {
            Player.Instance.ChangePlayerState(PlayerState.Fly);
            LeanTween.moveY(gameObject, 2f, .5f).setLoopPingPong(1).setOnComplete(() =>
            {
                var closestDistance = PathManager.Instance.Path.path.GetClosestPointOnPath(transform.position);
                if (CollectManager.Instance.CurrentWoodCount == 0 && Vector3.Distance(transform.position, closestDistance) > PathManager.Instance.PathMesh.roadWidth)
                {
                    GameManager.Instance.ChangeGameState(GameState.Lose);
                }
                else if (CollectManager.Instance.CurrentWoodCount > 0)
                {
                    ChangePlayerState(PlayerState.Run);
                }
            });

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
            if (GameManager.Instance.State != GameState.Starting)
                _transform.Rotate(Vector3.up * touch.deltaPosition.x * turnSpeed);
        }

        private void OnAfterStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.Starting:
                    currentSpeed = 0;
                    break;
                case GameState.Wait:
                    currentSpeed = 0;
                    break;
                case GameState.InGame:
                    gameObject.GetComponent<Animator>().SetTrigger("OnRun");
                    currentSpeed = speed;
                    ChangePlayerState(PlayerState.Run);
                    break;
                case GameState.Win:
                    currentSpeed = 0;
                    gameObject.GetComponent<Animator>().SetTrigger("OnStop");
                    break;
                case GameState.Lose:
                    currentSpeed = 0;
                    gameObject.GetComponent<Animator>().SetTrigger("OnStop");
                    break;
            }

        }


        #endregion
    }
}