using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureContainer : MonoBehaviour
{
    //Static List of all Treasure Containers for Win/Loss Condition Tracking
    public static List<TreasureContainer> allTreasureContainers = new List<TreasureContainer>();
    //What Type of Treasure
    public TreasureType treasureType;
    //How Much Treasure
    public int treasureAmount;

    private void Start()
    {
        //When Created, add this to the Static List
        allTreasureContainers.Add(this);
    }

    private void OnDestroy()
    {
        //When Destroyed, remove this from the Static List
        allTreasureContainers.Remove(this);
    }
}
