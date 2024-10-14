using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleUITest : MonoBehaviour
{
    [Inject]
    private ICmdSelectUIPrinter battleUIPrinter;

    class TestPawn : IPawn
    {
        public string Name => "ƒ|[ƒ“‘¾˜Y";

        public int MaxHP => 100;

        public int HP => 30;

        public float attack => 40;

        public float defence => 70;

        public float speed => 80;

        public float range => 10;

        public bool death => true;

        public Vector2 Position => throw new System.NotImplementedException();

        public int Mana => throw new System.NotImplementedException();
    }

    private TestPawn test;

    // Start is called before the first frame update
    void Start()
    {
        test = new TestPawn();
        //battleUIPrinter.PrintPlayerInformation(test);
        battleUIPrinter.PrintActionSelecter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
