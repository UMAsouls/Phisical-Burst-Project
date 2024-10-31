using System.Collections;
using UnityEngine;

public class PawnPackageStrage : MonoBehaviour
{

    public static PawnPackageStrage instance;

    [SerializeField]
    SerializedDictionary<GameObject> packages;

    public GameObject GetPawnPackage(string key) => packages[key];


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
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