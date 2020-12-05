using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;
    public Transform cubePrefab;

    [Range(0,1)]
    public float outlinePercent;

    public List<Coord> allTileCoords;
    private Dictionary<GameObject, Vector2> dict = new Dictionary<GameObject, Vector2>();
 
    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                //her bir tile in x,y olarak koordinati
                allTileCoords.Add(new Coord(x,y));
            }
        }

        string holderName = "Generated Map";
        if (transform.Find((holderName)))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;
        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                GameObject newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity).gameObject;
                dict.Add(newTile.gameObject, new Vector2(y,x) );
                newTile.gameObject.AddComponent<MeshCollider>();
                newTile.transform.localScale = Vector3.one * (1 - outlinePercent);
                newTile.transform.parent = mapHolder;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                GetNeighbourNegativeX(hitInfo.transform.gameObject);
                GetNeighbourPositiveX(hitInfo.transform.gameObject);
                GetNeighbourNegativeY(hitInfo.transform.gameObject);
                GetNeighbourPositiveY(hitInfo.transform.gameObject);

            }
        }
        
    }

    //coroutine kullan
    void GetNeighbourNegativeX(GameObject go)
    {
        GameObject id = go.transform.gameObject;
        Vector2 ss = dict[id];
        Vector2 neww = new Vector2(ss.x -1, ss.y);

        if (ss.x - 1 >= 0)
        {
            var neighbourGO = dict.FirstOrDefault(x => x.Value == neww).Key;
            neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            GetNeighbourNegativeX(neighbourGO);
            GetNeighbourPositiveX(neighbourGO);
            GetNeighbourNegativeY(neighbourGO);
            GetNeighbourPositiveY(neighbourGO);
        }
        
    }
    
    void GetNeighbourPositiveX(GameObject go)
    {
        GameObject id = go.transform.gameObject;
        Vector2 ss = dict[id];
        Vector2 neww = new Vector2(ss.x +1, ss.y);

        if (ss.x + 1 <= 9)
        {
            var neighbourGO = dict.FirstOrDefault(x => x.Value == neww).Key;
            neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            GetNeighbourPositiveX(neighbourGO);
        }
        
    }
    
    void GetNeighbourNegativeY(GameObject go)
    {
        GameObject id = go.transform.gameObject;
        Vector2 ss = dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y-1);
        Debug.Log(ss.y);

        if (ss.y - 1 >= 0)
        {
            var neighbourGO = dict.FirstOrDefault(x => x.Value == neww).Key;
            neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            GetNeighbourNegativeY(neighbourGO);
        }
        
    }
    
    void GetNeighbourPositiveY(GameObject go)
    {
        GameObject id = go.transform.gameObject;
        Vector2 ss = dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y+1);

        if (ss.y + 1 <= 9)
        {
            var neighbourGO = dict.FirstOrDefault(x => x.Value == neww).Key;
            neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            GetNeighbourPositiveY(neighbourGO);
        }
        
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
