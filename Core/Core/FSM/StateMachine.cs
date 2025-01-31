using System.Collections.Generic;

namespace Core.FSM
{
    public class StateMachine
    {
        private Dictionary<string, IState> states = new Dictionary<string, IState>();
        private IState currentState;
        private string currentStateName;

        public void AddState(string stateName, IState state)
        {
            states[stateName] = state;
        }

        public void RemoveState(string stateName)
        {
            if (currentStateName == stateName)
            {
                currentState?.OnExit();
                currentState = null;
                currentStateName = null;
            }
            states.Remove(stateName);
        }

        public void SwitchState(string newStateName)
        {
            if (!states.ContainsKey(newStateName)) return;
            if (currentStateName == newStateName) return;

            currentState?.OnExit();
            currentStateName = newStateName;
            currentState = states[newStateName];
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState?.OnUpdate();
        }

        public string GetCurrentState()
        {
            return currentStateName;
        }

        public void Clear()
        {
            currentState?.OnExit();
            currentState = null;
            currentStateName = null;
            states.Clear();
        }
    }
} 