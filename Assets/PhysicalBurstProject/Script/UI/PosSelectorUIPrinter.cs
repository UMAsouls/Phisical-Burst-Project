using System.Collections;
using UnityEngine;

public class PosSelectorUIPrinter : MonoBehaviour, IPosSelectorUIPrinter
{
    [SerializeField]
    private GameObject posSelectorUI;
    private GameObject printedSelector;

    public void DestroyPosSelectorUI()
    {
        if (printedSelector != null)
        {
            Destroy(printedSelector);
            printedSelector = null;
        }
    }

    public void PrintPosSelectorUI()
    {
        printedSelector = Instantiate(posSelectorUI);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}