using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using Zenject.SpaceFighter;
using Zenject;

public class TipsController : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> Tips = new List<GameObject>();

    [SerializeField]
    private GameObject SelectorController;
    private ICmdSelectorController controller;

    int tipsidx = 0;

    [Inject]
    SystemSEPlayable sePlayer;

    public void OnSelectorMove(InputValue input)
    {
        Vector2 moveInput = input.Get<Vector2>();

        //input上ではup > 0 down < 0
        if (moveInput.y > 0)
        {
            tipsidx = (int)Mathf.Repeat(tipsidx - 1, Tips.Count);
            controller.Set(tipsidx);
            Activate(tipsidx);
            sePlayer.SelectorMoveSE();
        }

        if (moveInput.y < 0)
        {
            tipsidx = (int)Mathf.Repeat(tipsidx + 1, Tips.Count);
            controller.Set(tipsidx);
            Activate(tipsidx);
            sePlayer.SelectorMoveSE();
        }
    }

    private void Awake()
    {
        foreach (var t in Tips) t.SetActive(false);
    }

    public int Length => Tips.Count;

    public void Activate(int idx)
    {
        for (int i = 0; i < Tips.Count; i++)
        {
            if (i == idx) Tips[i].SetActive(true);
            else Tips[i].SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        Activate(tipsidx);
        controller = SelectorController.GetComponent<ICmdSelectorController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}