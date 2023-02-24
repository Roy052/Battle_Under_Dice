using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    GameManager gm;
    [SerializeField] int buttonNum;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (buttonNum == 0)
            gm.SceneLoad_Match();
        else if (buttonNum == 1)
            gm.SceneLoad_CharacterSetUp();
        else if (buttonNum == 2)
            gm.GameQuit();
            
    }
}
