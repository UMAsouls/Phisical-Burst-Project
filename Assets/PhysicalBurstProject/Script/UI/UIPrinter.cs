using System.Collections;
using UnityEngine;
using Zenject;

public class UIPrinter : MonoBehaviour
{
    [Inject]
    DiContainer diContainer;

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

        // Use this for initialization
        void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}