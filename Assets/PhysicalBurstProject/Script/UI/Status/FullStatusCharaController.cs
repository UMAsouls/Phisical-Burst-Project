using System.Collections;
using UnityEngine;

public class FullStatusCharaController : MonoBehaviour
{

    [SerializeField]
    SerializedDictionary<GameObject> dict;

    private string now_active;

    public void Set(string name)
    {
        dict[now_active].SetActive(false); 
        dict[name].SetActive(true);
        now_active = name;
    }

    private void Awake()
    {
        foreach (var kvp in dict)
        {
            if (now_active == null) now_active = kvp.Key;
            kvp.Value.gameObject.SetActive(false);
        }
        Set(now_active);
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