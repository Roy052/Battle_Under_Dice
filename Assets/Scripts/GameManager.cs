using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int characterNum;
    public int[] skillSet = new int[6] { 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
