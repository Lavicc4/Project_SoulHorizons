using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Encounter", menuName = "Encounter")]
public class Encounter : ScriptableObject {

    [System.Serializable]
    public class Asset_Entry
    {
        public GameObject asset;
        public int x;
        public int y;
    }

    [System.Serializable]
    public class Terrain_Entry
    {
        //TODO: how does the terrain system work on the grid? This will decide what needs to go in this class.
        public string type;
        public int x;
        public int y;
    }
	public new string name;
    public string sceneName = "sn_GridTest";                        //Cameron made this variable.  It is used by the scene manager to go to a specific string.  For now all of our combat happens in GridTest, but that might change?

    [Header("Grid Size")]
    public int width = 3;
    public int length = 3;
	public bool completed; 
	public bool active;
    [Header("Terrain")]
    public string defaultTerrain;
    public List<Terrain_Entry> tiles = new List<Terrain_Entry>();
    [Header("Assets")]
    public List<Asset_Entry> enemies = new List<Asset_Entry>();
    public List<Asset_Entry> props = new List<Asset_Entry>();




}
