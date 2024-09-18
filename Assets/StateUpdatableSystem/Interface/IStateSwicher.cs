using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SUS;

namespace SUS
{
    public interface IStateSwitcher
    {
        public void Enable();
        public void Disable();
    }

    public abstract class AStateSwitcher : MonoBehaviour, IStateSwitcher
    {
        public abstract void Enable();
        public abstract void Disable();
    }
}


