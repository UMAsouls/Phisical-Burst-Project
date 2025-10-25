using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour, ITutorialUI
{
    [SerializeField]
    List<GameObject> Tutorials;

    List<GameObject> printingobjects;

    int idx = 0;

    private void SetEnable()
    {
        for(int i = 0; i < printingobjects.Count; i++)
        {
            if(idx == i) printingobjects[i].SetActive(true);
            else printingobjects[i].SetActive(false);
        }
    }

    public bool NextUI()
    {
        idx++;
        if(idx >= printingobjects.Count) return true;
        SetEnable();
        return false;
    }

    public bool PreviousUI()
    {
        idx--;
        if(idx < 0)
        {
            idx = 0;
            return true;
        }
        SetEnable();
        return false;
    }

    // Use this for initialization
    void Start()
    {
        printingobjects = new List<GameObject>();
        foreach(GameObject obj in Tutorials)
        {
            var p_obj = Instantiate(obj);
            p_obj.transform.SetParent(gameObject.transform, false);
            printingobjects.Add(p_obj);
        }
        idx = 0;
        SetEnable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool DestroyUI()
    {
        Destroy(gameObject);
        return true;
    }
}