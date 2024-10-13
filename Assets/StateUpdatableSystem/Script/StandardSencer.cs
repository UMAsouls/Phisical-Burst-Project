using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUS;

namespace SUS
{
    public class StandardSencer<T> : AStateSencer<T> where T : System.Enum
    {
        [SerializeField]
        private SerializedDictionary<
                T,
                AStateSwitcher
                > stateChangers;


        private T currentState;

        public override void StateChange(T stateID)
        {
            stateChangers[currentState].Disable();
            stateChangers[stateID].Enable();
            currentState = stateID;
        }

        public override void InitializeState(T state)
        {
            foreach (var kvp in stateChangers)
            {
                kvp.Value.gameObject.SetActive(false);
            }

            base.InitializeState(state);

            stateChangers[START_STATE].Enable();
            currentState = START_STATE;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


