using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Record
{
    public int win, lose, remainHp;

    public Record()
    {
        win = 0;
        lose = 0;
        remainHp = 0;
    }

    public Record(int win, int lose, int remainHp)
    {
        this.win = win;
        this.lose = lose;
        this.remainHp = remainHp;
    }
}

public class BattleRecord : MonoBehaviour
{
    [SerializeField] GameObject recordBoard;
    public void Set()
    {
        Record[] records = new Record[4];

        recordBoard.SetActive(true);
    }
}
