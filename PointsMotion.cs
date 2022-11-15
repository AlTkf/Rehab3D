using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMotion : MonoBehaviour
{
    public CoordData CoordinatesData;
    public GameObject[] BodyPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = CoordinatesData.data;
        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);
        string[] SingleCoordValues = data.Split(',');
        //print(data);
        //print(SingleCoordValues);

        for (int i = 0; i < 32; i++)
        {
            float x = float.Parse(SingleCoordValues[i * 3])/100-5;
            float y = float.Parse(SingleCoordValues[i * 3 + 1])/100;
            float z = float.Parse(SingleCoordValues[i * 3 + 2])/100;

            BodyPoints[i].transform.localPosition = new Vector3(x, y, z);
        }

    }
}
