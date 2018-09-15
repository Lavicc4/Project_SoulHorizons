﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//Colin 9/15/18

[System.Serializable]
/// <summary>
/// Stores all saved information about the state of the game.
/// Rather than creating everything at once, create new objects as it beconmes necessary to save data about them.
/// </summary>
public class GameState {

    PlayerState player;
    InventoryState inventory;
    List<RegionState> regions = new List<RegionState>();
    public bool lastGamePlayed;

    public GameState()
    {
        //creating a new save file; initialize things as needed
        //initialize player object
        player = new PlayerState();
        player.playerLevel = 1;

        //TODO: initialize inventory
        inventory = new InventoryState();

        //TODO: initialize anything else?
    }
    

    /*Player related methods */
    public int GetPlayerLevel()
    {
        return player.playerLevel;
    }

    public string GetPlayerName()
    {
        return player.name;
    }


    /*Region specific methods. All of these take the region name as an argument*/
    /// <summary>
    /// This will run at the start of all the public methods to find the region's state. If no state exists,
    /// then load the information about the initial state of the region from a file and create the RegionState object.
    /// </summary>
    private RegionState GetRegionState(string regionName)
    {
        foreach (RegionState r in regions)
        {
            if (r.name.Equals(regionName))
            {
                return r;
            }
        }

        //if we get here, the player is entering the region for the first time, so no state exists
        //load the initial region information from a file and create the RegionState object with it
        StreamReader file = new StreamReader(Path.Combine("Assets/Scripts/Combat/Deck/Deck Lists", regionName + ".txt"));
        if (file == null)
        {
            Debug.Log("File did not open");
            return null;
        }
        
        RegionState region = new RegionState();
        //TODO: parse the txt file

        return region;
    }

    public List<string> GetEncounterPool(string region)
    {
        RegionState r = GetRegionState(region);
        if(r != null)
        {
            return r.encounterPool;
        }
        return null;
    }


}

[System.Serializable]
/// <summary>
/// Stores all saved information about a specific region.
/// </summary>
public class RegionState
{
    public string name; //use this to find the region
    public List<string> encounterPool; //contains all of the available encounters in this region
}

[System.Serializable]
/// <summary>
/// Stores all saved information about the player.
/// </summary>
public class PlayerState
{
    public int playerLevel;
    internal string name;
}

[System.Serializable]
public class InventoryState
{

}
