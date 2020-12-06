using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCubes : MonoBehaviour
{
    private MapGenerator map;
    private GameObject enemyObject;
    public Vector2 enemyPosition;
    public float cubeOpeningTime;

    private void OnEnable()
    {
        PlayerCube.OnEnemyCubePlacement += StartCubePlacement;
    }
    private void OnDisable()
    {
        PlayerCube.OnEnemyCubePlacement -= StartCubePlacement;
    }


    private void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        CreateEnemy();
    }

    void CreateEnemy()
    {
        enemyObject = map.dict.FirstOrDefault(x => x.Value == enemyPosition).Key;
        enemyObject.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    void StartCubePlacement()
    {
        StartCoroutine(GetEnemyNeighbourNegativeX(enemyObject));
        StartCoroutine(GetEnemyNeighbourPositiveX(enemyObject));
        StartCoroutine(GetEnemyNeighbourNegativeY(enemyObject));
        StartCoroutine(GetEnemyNeighbourPositiveY(enemyObject));
    }
    
    
    
    IEnumerator GetEnemyNeighbourNegativeX(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x -1, ss.y);

        if (ss.x - 1 >= 0)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            
            StartCoroutine(GetEnemyNeighbourNegativeX(neighbourGO));
            StartCoroutine(GetEnemyNeighbourNegativeY(neighbourGO));
            StartCoroutine(GetEnemyNeighbourPositiveY(neighbourGO));
        }


    }
    
    IEnumerator GetEnemyNeighbourPositiveX(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x +1, ss.y);

        if (ss.x + 1 <= 9)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            
            StartCoroutine(GetEnemyNeighbourPositiveX(neighbourGO));
            StartCoroutine(GetEnemyNeighbourNegativeY(neighbourGO));
            StartCoroutine(GetEnemyNeighbourPositiveY(neighbourGO));

        }


    }
    
    IEnumerator GetEnemyNeighbourNegativeY(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y-1);

        if (ss.y - 1 >= 0)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            StartCoroutine(GetEnemyNeighbourNegativeY(neighbourGO));

        }
    }
    
    IEnumerator GetEnemyNeighbourPositiveY(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y+1);

        if (ss.y + 1 <= 9)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            StartCoroutine(GetEnemyNeighbourPositiveY(neighbourGO));

        }
    }
}
