using System.Collections;
using UnityEngine;

public class CommandStrage : MonoBehaviour
{

    [SerializeField]
    SerializedDictionary<IActionCommand> actionCmds;

    [SerializeField]
    SerializedDictionary<IBattleCommand> battleCmds;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}