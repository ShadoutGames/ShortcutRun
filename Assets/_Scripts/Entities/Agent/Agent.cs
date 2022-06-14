using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool trial;

    private float _speed;
    private StateMachine stateMachine;
    private List<IState> states;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitStateMachine();
        GameManager.AfterStateChanged += OnAfterStateChanged;
        speed = _speed;
    }


    private void Update()
    {
        rb.velocity = transform.forward * _speed;
        stateMachine.Tick();
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine();
        states = new List<IState>();

        var runState = new RunState();
        var turnState = new TurnState(this);

        states.Add(runState);
        states.Add(turnState);

        stateMachine.AddTransition
        (
            runState,
            turnState,
            () => trial
        );

        stateMachine.AddTransition
        (
            turnState,
            runState,
            () => !trial
        );

        stateMachine.SetState(runState);
    }
    private void OnAfterStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Starting:
                break;
            case GameState.Wait:
                _speed = 0;
                break;
            case GameState.InGame:
                LeanTween.delayedCall(3f, () => _speed = speed);
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
    }
}
