using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoCmdText : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    LayoutElement textLayout;

    [SerializeField]
    GameObject SelectAllow;

    [SerializeField]
    LayoutElement allowSpacer;

    [SerializeField]
    int TextSize;

    public void SetText(string cmdText)
    {
        text.text = cmdText;
        textLayout.preferredWidth = cmdText.Length*TextSize;
    }

    public void Select() => SelectAllow.SetActive(true);
    public void UnSelect() => SelectAllow.SetActive(false);

    private void Awake()
    {
        SelectAllow.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        text.fontSize = TextSize;
        allowSpacer.preferredWidth = TextSize;
        allowSpacer.preferredHeight = TextSize;
        allowSpacer.minHeight = TextSize;

        textLayout.preferredHeight = TextSize;
        textLayout.minHeight = TextSize;
    }

    // Update is called once per frame
    void Update()
    {

    }
}