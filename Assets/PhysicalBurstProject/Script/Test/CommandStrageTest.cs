using System.Collections;
using UnityEngine;

namespace Assets.PhysicalBurstProject.Script.Test
{
    public class CommandStrageTest : MonoBehaviour
    {

        [SerializeField]
        private CommandStrage2 CommandStrage2;
        ICommandStrage commandStrage;

        // Use this for initialization
        void Start()
        {
            commandStrage = CommandStrage2;

            Debug.Log(commandStrage.GetBattleCommand("防御").Name);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}