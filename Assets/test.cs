using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    GameObject cubesPrefab;

    [SerializeField]
    GameObject[] cubesArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (cubesArray.Length >= 10)
            {
                RemoveLeftCube();
            }

            AddRightCube();
        }
    }

    void AddRightCube()
    {
        GameObject NewGo = Instantiate(cubesPrefab, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), Quaternion.identity);

        NewGo.name += NewGo.transform.position;

        var temp = new List<GameObject>(cubesArray);
        temp.Add(NewGo);
        cubesArray = temp.ToArray();
    }

    void RemoveLeftCube()
    {

        var temp = new List<GameObject>(cubesArray);
        Destroy(temp[0]);
        temp.RemoveAt(0);

        cubesArray = temp.ToArray();
    }
}
