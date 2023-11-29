using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    public float vitesseD;
    float forceDeplacement;

    public float vitesseTourne;
    public float duration;

    public GameObject CamLight;
    public GameObject MainCam;

    public GameObject fermeA;
    public GameObject fermeA2;
    public GameObject allumeA;
    public GameObject allumeA2;

    public GameObject fermeB;
    public GameObject fermeB2;
    public GameObject allumeB;
    public GameObject allumeB2;

    public GameObject porte;
    public GameObject cle;
    private bool hasKey = false;

    //voir si le joueur est en collision avec la lumiere
    private bool zoneJoueurLumiere = false;


    // Start is called before the first frame update
    void Start()
    {
        //on voit pas la souris
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, forceDeplacement, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        forceDeplacement = Input.GetAxis("Vertical") * vitesseD;
        // GetComponent<Animator>().SetBool("marche", true);
        GetComponent<Animator>().SetFloat("vitesse", GetComponent<Rigidbody>().velocity.magnitude);


        float valeurTourne = Input.GetAxis("Mouse X") * vitesseTourne;
        transform.Rotate(0f, valeurTourne, 0f);

        if (Input.GetKeyDown(KeyCode.E) && zoneJoueurLumiere == true)
        {
            // perso interragi avec la lumiere
            InteractionObj();
        }
    }

    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "cle")
        {
            Destroy(infoCollider.gameObject);
            //camera s'active et se désactive après 4s
            CamLight.SetActive(true);
            //GetComponent<AudioSource>().PlayOneShot(cleSon);
        }

        // verifier si joueur est en collision
        if (infoCollider.gameObject == gameObject)
        {
            zoneJoueurLumiere = true;
        }
    }

    void InteractionObj()
    {
        // verifier si la lumiere est bien ferme
        if (fermeA != null && fermeA.activeSelf)
        {
            // désactiver les lumieres fermées et activer celles ouvertes
            fermeA.SetActive(false);
            allumeA.SetActive(true);
            fermeA2.SetActive(false);
            allumeA2.SetActive(true);
            //camera 2 s'active 
            CamLight.SetActive(true);
            //et se désactive après 4s
            Invoke("SwitchCam", 4f);
            //GetComponent<AudioSource>().PlayOneShot(lumSon);
        }

        if (fermeB != null && fermeB.activeSelf)
        {
            // désactiver les lumieres fermées et activer celles ouvertes
            fermeB.SetActive(false);
            allumeB.SetActive(true);
            fermeB2.SetActive(false);
            allumeB2.SetActive(true);

            //camera 2 s'active
            CamLight.SetActive(true);
            //et se désactive après 4s
            Invoke("SwitchCam", 4f);
            //GetComponent<AudioSource>().PlayOneShot(lumSon);
        }

        // Check if the player has the key
        if (hasKey)
        {
            // si fermeA et fermeB sont désactivé
            if (!fermeA.activeSelf && !fermeB.activeSelf)
            {
                // et si porte is not null, on peut sortir
                if (porte != null)
                {
                    porte.SetActive(true);
                }
            }
        }

        else
        {
            // sinon, le joueur doit ramasser la cle
            RamasseCle();
        }
    }

    void RamasseCle()
    {
        // si la cle est encore la
        if (cle != null && cle.activeSelf)
        {
            //  
            hasKey = true;
            // Deactivate the cle
            cle.SetActive(false);
            // son cle
            //GetComponent<AudioSource>().PlayOneShot(cleSon);
        }
    }

    void SwitchCam()
    {
        CamLight.SetActive(false);
    }
}