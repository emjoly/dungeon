using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debut : MonoBehaviour
{
    public GameObject objetANePasDetruir;
    public static bool dontDestroyDejaFait = false;

    void Start()
    {
        if (dontDestroyDejaFait == false)         //la 1e fois qu'on fait le DontDestroy
        {
            DontDestroyOnLoad(objetANePasDetruir);
            dontDestroyDejaFait = true;
        }
        else                                      //c'est déjà fait alors efface le doublon
        {
            Destroy(objetANePasDetruir);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Jeu");
        }
    }
}
