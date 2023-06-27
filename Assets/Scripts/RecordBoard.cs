using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordBoard : MonoBehaviour
{
    public Text[] wins;
    public Text[] loses;
    public Text[] remainHps;
    public void Set(Record[] records)
    {
        for(int i = 0; i < records.Length; i++)
        {
            wins[i].text = records[i].win.ToString();
            loses[i].text = records[i].lose.ToString();
            remainHps[i].text = records[i].remainHp.ToString();
        }
    }
}
