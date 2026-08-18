using System.Collections;
using UnityEngine;
using Zenject;

public class UIPrinter : MonoBehaviour
{
    [Inject]
    DiContainer diContainer;

    protected const int WINDOW_WIDTH = 2560;
    protected const int WINDOW_HEIGHT = 1440;

    protected GameObject PrintUIAsChild(GameObject obj)
    {
        var obj1 =  diContainer.InstantiatePrefab(obj);
        obj1.transform.SetParent(transform, false);

        return obj1;
    }

    protected GameObject PrintUIAsChildAt(GameObject obj, Vector2 pos)
    {
        var obj1 = PrintUIAsChild(obj);

        RectTransform rect = obj1.GetComponent<RectTransform>();
        rect.anchoredPosition = pos;

        return obj1;
    }

    protected Vector2 WorldToUIPoint(Vector2 pos)
    {
        Vector2 uiPos = Camera.main.WorldToScreenPoint(pos);
        uiPos.x = uiPos.x  / Screen.width * WINDOW_WIDTH;
        uiPos.y = -1 * (WINDOW_HEIGHT -  uiPos.y  / Screen.height * WINDOW_HEIGHT);
        return uiPos;
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