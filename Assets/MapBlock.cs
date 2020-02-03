using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySettings", menuName = "My Asset/Data Setting", order = 1)]
public class MapBlock : ScriptableObject
{
    public bool[] MapDetails;

    public List<int> ConToInt()
    {
        List<int> RowData = new List<int>();
        for (int i = 0; i < MapDetails.Length; i++)
        {
            RowData.Add(MapDetails[i]? 0 : 1 );
        }
        return RowData;
    }
}
