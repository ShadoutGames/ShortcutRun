using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : IState
{
    public TurnState(Agent agent)
    {
        this.agent = agent;
    }

    private Agent agent;

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        agent.transform.Rotate(Vector3.up * 1);
    }
}
