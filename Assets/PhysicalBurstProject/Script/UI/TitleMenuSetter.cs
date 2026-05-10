using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TitleMenuSetter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var setter = GetComponentInChildren<ICmdUI>();

        setter.CmdAdd("プレイ");
        setter.CmdAdd("終了");

        var controller = GetComponentInChildren<ICmdSelectorController>();

        controller.Set(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}