using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TreasureType
{
    None,
    Coins,
    PileOfCoins
}

[RequireComponent(typeof(TreasureContainer))]
public class Treasure : MonoBehaviour
{
    //Store list of Treasures for Enemy Pathfinding
    public static List<Treasure> treasureList = new List<Treasure>();
    //Container for Treasure Values
    public TreasureContainer treasureContainer;

    //Sprites for different Treasure Types
    public Sprite treasureSprite;
    public Sprite coinSprite;

    //Call after Instantiating to create a Treasure
    public void CreateTreasure(TreasureType _tt)
    {
        //Update Treasure Container Type
        treasureContainer.treasureType = _tt;
        //Add Treasure to Static List
        treasureList.Add(this);
        //Create Treasure values based on Type
        switch (_tt)
        {
            case TreasureType.Coins:
                {
                    treasureContainer.treasureAmount = 100;
                    GetComponentInChildren<SpriteRenderer>().sprite = coinSprite;
                    break;
                }
            case TreasureType.PileOfCoins:
                {
                    treasureContainer.treasureAmount = 1000;
                    GetComponentInChildren<SpriteRenderer>().sprite = treasureSprite;
                    transform.localScale = new Vector2(1.5f, 1.5f);
                    break;
                }
            default:
                {
                    Debug.LogError("Critical Logic Error - Missing Treasure Type");
                    break;
                }
        }
    }

    //When Treasure is picked up be the Enemy
    public void LootTreasure()
    {
        //Remove from pathfinding list
        treasureList.Remove(this);
        Destroy(gameObject);
    }
}
