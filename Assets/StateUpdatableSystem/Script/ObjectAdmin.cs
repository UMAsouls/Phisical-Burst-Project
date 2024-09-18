using SUS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace SUS
{
    public class ObjectAdmin : AStateSwitcher
    {
        [SerializeField]
        private AStateBehaviour[] stateBehaviours;

        public override void Disable()
        {
            foreach (var stateBehaviour in stateBehaviours)
            {
                stateBehaviour.StateEnd();
            }
            gameObject.SetActive(false);
        }

        public override void Enable()
        {
            gameObject.SetActive(true);
            foreach (var stateBehaviour in stateBehaviours)
            {
                stateBehaviour.StateStart();
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            foreach (var stateBehaviour in stateBehaviours)
            {
                stateBehaviour.StateUpdate();
            }
        }
    }
}


