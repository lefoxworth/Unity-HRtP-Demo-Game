using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{

    public Text text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (text.text == "Congratulations!")
        {
            StartCoroutine(LoadNext());
        }
    }

    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("End");
    }
}
