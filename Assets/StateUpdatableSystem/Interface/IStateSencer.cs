using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SUS;

namespace SUS
{
    public interface IStateSencer<T> where T : System.Enum
    {
        public void StateChange(T state);

        public void InitializeState(T state);
    }

    public abstract class AStateSencer<T> : MonoBehaviour, IStateSencer<T> where T : System.Enum
    {

        protected T START_STATE;
        public abstract void StateChange(T state);

        public virtual void InitializeState(T state)
        {
            START_STATE = state;
        }
    }
}


