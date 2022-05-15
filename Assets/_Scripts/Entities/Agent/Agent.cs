using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool trial;

    private StateMachine stateMachine;
    private List<IState> states;
    private Rigidbody rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        InitStateMachine();
    }

    private void Update() 
    {
        rb.velocity = transform.forward * speed;
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
            ()=> trial
        );

        stateMachine.AddTransition
        (
            turnState,
            runState,
            ()=> !trial
        );

        stateMachine.SetState(runState);
    }
}
