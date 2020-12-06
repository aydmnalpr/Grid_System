using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCube : MonoBehaviour
{
    private MapGenerator map;

    private Camera camera;
    public float cubeOpeningTime;

    public static Action OnEnemyCubePlacement = delegate {  };


    private void Start()
    {
        camera = Camera.main;
        map = FindObjectOfType<MapGenerator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                StartCoroutine(GetNeighbourNegativeX(hitInfo.transform.gameObject));
                StartCoroutine(GetNeighbourPositiveX(hitInfo.transform.gameObject));
                StartCoroutine(GetNeighbourNegativeY(hitInfo.transform.gameObject));
                StartCoroutine(GetNeighbourPositiveY(hitInfo.transform.gameObject));
                
                OnEnemyCubePlacement.Invoke();
                


            }
        }
    }
    
    IEnumerator GetNeighbourNegativeX(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x -1, ss.y);

        if (ss.x - 1 >= 0)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;

            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            StartCoroutine(GetNeighbourNegativeX(neighbourGO));
            StartCoroutine(GetNeighbourNegativeY(neighbourGO));
            StartCoroutine(GetNeighbourPositiveY(neighbourGO));
        }


    }
    
    IEnumerator GetNeighbourPositiveX(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x +1, ss.y);

        if (ss.x + 1 <= 9)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
            StartCoroutine(GetNeighbourPositiveX(neighbourGO));
            StartCoroutine(GetNeighbourNegativeY(neighbourGO));
            StartCoroutine(GetNeighbourPositiveY(neighbourGO));
        }
    }
    
    IEnumerator GetNeighbourNegativeY(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y-1);

        if (ss.y - 1 >= 0)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            StartCoroutine(GetNeighbourNegativeY(neighbourGO));

        }
    }
    
    IEnumerator GetNeighbourPositiveY(GameObject go)
    {
        yield return new WaitForSeconds(cubeOpeningTime);
        GameObject id = go.transform.gameObject;
        Vector2 ss = map.dict[id];
        Vector2 neww = new Vector2(ss.x, ss.y+1);

        if (ss.y + 1 <= 9)
        {
            var neighbourGO = map.dict.FirstOrDefault(x => x.Value == neww).Key;
            if (neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
            {
                neighbourGO.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            StartCoroutine(GetNeighbourPositiveY(neighbourGO));

        }
    }
}
