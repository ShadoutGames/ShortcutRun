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

        private Rigidbody rb;
        private Transform _transform;
        private float distanceTravelled;
        private float currentSpeed;
        private float time;

        #endregion

        #region Props

        #endregion
        #region Actions
        public event Action WayOut;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Start()
        {
            InputSystem.Instance.TouchPositionChanged += OnTouchPositionChanged;

            currentSpeed = speed;
        }

        private void Update()
        {
            time -= Time.deltaTime;
            rb.velocity = _transform.forward * speed;

            var closestDistance = PathManager.Instance.Path.path.GetClosestPointOnPath(transform.position);
            if (Vector3.Distance(transform.position, closestDistance) > PathManager.Instance.PathMesh.roadWidth)
            {
                Debug.Log("asd");
                WayOut?.Invoke();
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

        #endregion

        #region Callbacks

        private void OnTouchPositionChanged(Touch touch)
        {
            _transform.Rotate(Vector3.up * touch.deltaPosition.x * turnSpeed);
        }

        #endregion
    }
}