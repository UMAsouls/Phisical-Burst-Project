using System.Collections;
using TMPro;
using UnityEngine;

public class CmdInfoWindow : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameUI;

    [SerializeField]
    TextMeshProUGUI typeUI;

    [SerializeField]
    TextMeshProUGUI descriptionUI;

    [SerializeField]
    TextMeshProUGUI useManaUI;

    public string Name { set =>  nameUI.text = value; }

    public string Type { set => typeUI.text = "分類: " + value; }

    public string Description { set =>  descriptionUI.text = value; }

    public string UseMana { set => useManaUI.text = "消費魔力: " + value; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}