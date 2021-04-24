using System;
using System.Collections.Generic;
using UnityEngine;

class MooreMachine<State, Trigger>
{
    private readonly Dictionary<State, Action> _actions;
    private readonly Dictionary<(State, Trigger), State> _transitions;
    private State _currentState;

    private MooreMachine(State initialState, Dictionary<State, Action> actions, Dictionary<(State, Trigger), State> transitions)
    {
        _actions = actions;
        _transitions = transitions;
        _currentState = initialState;
    }

    public bool Transition(Trigger trigger)
    {
        (State, Trigger) key = (_currentState, trigger);
        State nextState;
        Action action;

        if (!_transitions.TryGetValue(key, out nextState))
        {
            Debug.Log($"There is no transition from state {_currentState} on trigger {trigger}.");
            return false;
        }
        _currentState = nextState;

        if (!_actions.TryGetValue(nextState, out action))
        {
            Debug.Log($"No action associated with state {nextState}.");
            return false;
        }
        action();
        return true;
    }

    public static MooreMachineBuilder Create(State initialState)
    {
        return new MooreMachineBuilder(initialState);
    }

    public class MooreMachineBuilder
    {
        private readonly Dictionary<State, Action> _actions;
        private readonly Dictionary<(State, Trigger), State> _transitions;
        private readonly State _initialState;
        private State _currentState;

        public MooreMachineBuilder(State initialState)
        {
            _actions = new Dictionary<State, Action>();
            _transitions = new Dictionary<(State, Trigger), State>();
            _currentState = _initialState = initialState;
        }

        public MooreMachineBuilder Configure(State state)
        {
            _currentState = state;
            return this;
        }

        public MooreMachineBuilder Do(Action action)
        {
            _actions.Add(_currentState, action);
            return this;
        }

        public MooreMachineBuilder CanTransitionOn(Trigger trigger, State nextState)
        {
            _transitions.Add((_currentState, trigger), nextState);
            return this;
        }

        public MooreMachine<State, Trigger> Build()
        {
            return new MooreMachine<State, Trigger>(_initialState, _actions, _transitions);
        }
    }
}