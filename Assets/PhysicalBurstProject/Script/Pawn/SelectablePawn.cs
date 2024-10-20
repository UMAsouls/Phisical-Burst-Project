using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SelectablePawn : MonoBehaviour
{
    [SerializeField]
    GameObject FocusArrow;
    private GameObject printedArrow;

    [SerializeField]
    float SelectableSize;

    [SerializeField]
    GameObject colliderObj;

    public void OnFocus()
    {
        Vector2 arrowPos = transform.position;
        arrowPos.y += SelectableSize/2;

        printedArrow = Instantiate(FocusArrow, arrowPos, Quaternion.identity);
    }

    public void OnUnFocus()
    {
        if(printedArrow != null)  Destroy(printedArrow);
    }


    // Use this for initialization
    void Start()
    {
        colliderObj.transform.localScale = (Vector3)Vector2.one * SelectableSize;
        colliderObj.transform.localScale += new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}