using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//Colin 9/9/18

[System.Serializable]
/// <summary>
/// Stores all saved information about the state of the game.
/// </summary>
public class GameState {

    PlayerState player;
    InventoryState inventory;
    List<RegionState> regions = new List<RegionState>();
    

    /*Player related methods */
    public int GetPlayerLevel()
    {
        return player.playerLevel;
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
}

[System.Serializable]
public class InventoryState
{

}
