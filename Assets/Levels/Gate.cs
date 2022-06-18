using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{

    private GameObject gate;

    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        gate = GameObject.Find("Gate");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(nextLevel);
    }

}
