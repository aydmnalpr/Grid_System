using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;
    private Transform mapHolder;

    [Range(0,1)]
    public float outlinePercent;

    private List<Coord> allTileCoords = new List<Coord>();
    [NonSerialized]public Dictionary<GameObject, Vector2> dict = new Dictionary<GameObject, Vector2>();
    
 
    private void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        MakeParentObjectForClones();
        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x,y));
                
                Vector3 tilePosition = CoordToPosition(x, y);
                GameObject newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity).gameObject;
                dict.Add(newTile.gameObject, new Vector2(y,x) );
                newTile.gameObject.AddComponent<MeshCollider>();
                newTile.transform.localScale = Vector3.one * (1 - outlinePercent);
                newTile.transform.parent = mapHolder;
            }
        }
    }

    private void MakeParentObjectForClones()
    {
        string holderName = "Generated Map";
        
        if (transform.Find((holderName)))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        
        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
    }

    
    
    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public struct  Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
