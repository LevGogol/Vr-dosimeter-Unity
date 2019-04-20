using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentState;
    private List<State> states = new List<State>();
        
    public delegate bool Condition();
    public delegate void Payload();
        
    private bool isStopAfterMove = true;

    public StateMachine(){ }

    public StateMachine Enter(State state)
    {
        if (!states.Contains(state))
        {
            states.Add(state);
        }
            
        currentState = state;
        return this;
    }

    public StateMachine Move()
    {
        State nextState = currentState;
        while (true)
        {
            nextState = nextState.Move();
            if (nextState == null)
            {
                break;
            }

            currentState = nextState;
            isStopAfterMove = true;
        }
        return this;
    }

    public StateMachine MoveAfter(int millisecond, Payload payload)
    {
        isStopAfterMove = false;
        
        TasksPool.Instance.Add(TasksPool.Instance.NowTime.AddMilliseconds(millisecond), () =>
        {
            payload();
            Move();
        });
        return this;
    }

    public StateMachine Add(State state)
    {
        states.Add(state);
        return this;
    }

    public class State
    {
        private readonly List<Transition> transitions = new List<Transition>();

        public State(Transition[] transitions)
        {
            if (transitions == null)
            {
                throw new ArgumentNullException();
            }
                
            this.transitions.AddRange(transitions);
        }

        public State(){}

        public State Add(Transition transition)
        {
            transitions.Add(transition);
            return this;
        }
                
        public State Move()
        {
            foreach (var transition in transitions)
            {
                if (transition.IsCondition())
                {
                    transition.Payload();
                    return transition.GetNextState();
                }
            }

            return null;
        }
    }

    public class Transition
    {
        private Condition condition;
        private Payload payload;
        private State nextState;

        public Transition(Condition condition, Payload payload, State nextState)
        {
            this.condition = condition;
            this.payload = payload;
            this.nextState = nextState;
        }

        public bool IsCondition() => condition();

        public void Payload() => payload();

        public State GetNextState() => nextState;
    }
}