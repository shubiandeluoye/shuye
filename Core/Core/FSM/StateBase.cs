namespace Core.FSM
{
    public abstract class StateBase : IState
    {
        protected StateMachine stateMachine;

        public StateBase(StateMachine machine)
        {
            stateMachine = machine;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
} 