using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SUS;

namespace SUS
{
    public interface IStateBehaviour
    {
        public void StateStart();
        public void StateUpdate();
        public void StateEnd();
    }

    public abstract class AStateBehaviour : MonoBehaviour, IStateBehaviour
    {
        public abstract void StateStart();
        public abstract void StateUpdate();
        public abstract void StateEnd();
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



