using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CommandAdder : MonoBehaviour, ICommandAdder
{

    private static CommandAdder instance;
    public static CommandAdder Instance => instance;
    public bool Inited { get; private set; }

    public Dictionary<string, AddCommand> addCommands;

    private string nextScene;

    public AddCommand GetCommandList(string name)
    {
        if(!addCommands.ContainsKey(name)) return new AddCommand(0);
        return addCommands[name];
    }

    private void Awake()
    {
        MakeInstance();
    }

    public void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                Destroy(this);
            }
        }
    }

    private void Init()
    {
        if (Inited == true) return;
        addCommands = new Dictionary<string, AddCommand>();

        Inited = true;
    }

    public void AddCommand(string name, int cmd_idx, CommandType type)
    {
        if(!addCommands.ContainsKey(name))
        {
            addCommands[name] = new AddCommand(0);
        }

        switch(type)
        {
            case CommandType.ActionCommand:
                addCommands[name].AddActionCmdList.Add(cmd_idx);
                break;
            case CommandType.BattleCommand:
                addCommands[name].AddBattleCmdList.Add(cmd_idx);
                break;
        }
        
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoAddCommand(string nextScene)
    {
        if (nextScene == "Title") SceneManager.LoadScene("Title");
        this.nextScene = nextScene;
        SceneManager.LoadScene("AddCommand");
    }

    public void GoNextScene()
    {
        Debug.Log(nextScene);
        if (nextScene == null || nextScene == "") SceneManager.LoadScene("Title");
        else SceneManager.LoadScene(nextScene);
    }
}