using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchSM : MonoBehaviour
{
    GameManager gm;

    [SerializeField] Text matchText;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.isAI = 1;

        StartCoroutine(Matching());
    }

    IEnumerator Matching()
    {
        yield return new WaitForSeconds(1);

        matchText.text = "Matching";

        yield return new WaitForSeconds(2);

        matchText.text = "Matched";

        yield return new WaitForSeconds(1);

        gm.SceneLoad_Battle();
    }

}
