using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSavannah
{
    class StateMachine<T>
    {
        private T entity;
        private State<T> currentState;

        public StateMachine(T ent, State<T> startstate)
        {
            entity = ent;
            currentState = startstate;
            startstate.Enter(entity);

        } 
        public void ChangeState(State<T> newstate)
        {
            currentState.Enter(entity);
            currentState = newstate;
            currentState.Exit(entity);
        }

        public void Update()
        {
            currentState.Execute(entity);
        }
    }
}
