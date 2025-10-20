using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelectUnit : MonoBehaviour, IObservable<SelectPhaseFrag>
{
    IVirtualPawnManager vpawnManager;

    IPawnActionManager pawnActionManager;

    List<IObserver<SelectPhaseFrag>> selectPhaseObservers;

    public bool CancelSelect()
    {
        throw new System.NotImplementedException();
    }

    public UniTask DoAction()
    {
        throw new System.NotImplementedException();
    }

    public void SelectEnd()
    {
        vpawnManager.VirtualPawnDestroy();
        ObserverSupport.BroadCastMessage(
            selectPhaseObservers, SelectPhaseFrag.SelectEnd
        );
    }

    public void SelectStart()
    {
        vpawnManager.VirtualPawnSet();
        ObserverSupport.BroadCastMessage(
            selectPhaseObservers, SelectPhaseFrag.SelectStart
        );
    }

    public void EmergencyBattle()
    {
        
    }



    // Use this for initialization
    void Start()
    {
        selectPhaseObservers = new List<IObserver<SelectPhaseFrag>>();
        vpawnManager = GetComponent<IVirtualPawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Subscribe(IObserver<SelectPhaseFrag> observer)
    {
        selectPhaseObservers.Add(observer);
    }
}