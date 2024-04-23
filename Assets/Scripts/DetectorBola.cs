using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DetectorBola : MonoBehaviour
{
    public GameObject[] bolos;
    public float gradosCaida = 45f;
    private int bolosCaidos;
    public int lanzamiento;
    public XRBaseInteractor rHand;
    public XRBaseInteractor lHand;
    public TextMeshProUGUI lanzamientos;
    public TextMeshProUGUI bolosCaidosText;
    public TextMeshProUGUI puntuacionText;
    public TextMeshProUGUI nlanzamientoText;
    private int nlanzamiento;
    //public GameObject boloCaidoPrefab;
    public int puntuacionTotal = 0;
    private bool tirada;
    // Start is called before the first frame update
    void Start()
    {
        nlanzamiento = 1;
        bolosCaidos = 0;
        lanzamiento = 0;
        tirada = false;
        bolos = GameObject.FindGameObjectsWithTag("Bolos");
    }

    // Update is called once per frame
    void Update()
    {
        lanzamientos.text = lanzamiento.ToString();
        bolosCaidosText.text = bolosCaidos.ToString();
        puntuacionText.text = puntuacionTotal.ToString();

    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        lanzamiento++;
        
        yield return new WaitForSeconds(5);
        if (other.gameObject.tag == "Bola" && !tirada) {
            bolosCaidos = 0;
            Destroy(other.gameObject);  
            tirada=true;
            Debug.Log("Bola ha llegado a los bolos");
            DetectarCaida(bolos);
            ControlarLanzamiento();
   
        }
    }

    private void DetectarCaida(GameObject[] bolos)
    {
        for (int i = 0; i < bolos.Length; i++)
        {
            if (bolos[i])
            {
                if (bolos[i].transform.up.y < 0.5 && !bolos[i].GetComponent<Bolo>().boloCaido)
                {
                    bolos[i].GetComponent<Bolo>().boloCaido = true;
                    bolos[i].SetActive(false);
                    bolosCaidos++;
                    Debug.Log(bolosCaidos);
                }

            }
        }

        if (nlanzamiento == 2 || bolosCaidos == 10)
        {
            nlanzamiento = 1;
            puntuacionTotal += bolosCaidos;
            ResetBolosTodos();
            bolosCaidos = 0;
            tirada = false;
            nlanzamientoText.text = "Primer lanzamiento";
        }
        else if (bolosCaidos >= 0 && bolosCaidos < 10)
        {
            nlanzamiento++;
            nlanzamientoText.text = "Segundo lanzamiento";
            ResetBolosNoCaidos();
            puntuacionTotal += bolosCaidos;
            tirada = false;
        }

    }



    private IEnumerator ControlarLanzamiento()
    {
        rHand.gameObject.SetActive(false);
        lHand.gameObject.SetActive(false);
        for (int i = 0; i < bolos.Length; i++)
        {
            bolos[i].SetActive(false);
        }
        yield return new WaitForSeconds(5);
        rHand.gameObject.SetActive(true);
        lHand.gameObject.SetActive(true);
        for (int i = 0; i < bolos.Length; i++)
        {
            bolos[i].SetActive(true);
        }
    }



    private void ResetBolosNoCaidos()
    {
        for (int i = 0; i < bolos.Length; i++)
        {
            if (!bolos[i].GetComponent<Bolo>().boloCaido)
            {
                bolos[i].SetActive(true);
                bolos[i].transform.SetPositionAndRotation(bolos[i].GetComponent<Bolo>().posicionIniPos, bolos[i].GetComponent<Bolo>().posicionIniRot);
                bolos[i].GetComponent<Bolo>().boloCaido = false;
            }
            
        }
    }



    private void ResetBolosTodos()
    {
        for (int i = 0; i < bolos.Length; i++)
        {

            bolos[i].SetActive(true);
            bolos[i].transform.SetPositionAndRotation(bolos[i].GetComponent<Bolo>().posicionIniPos, bolos[i].GetComponent<Bolo>().posicionIniRot);
            bolos[i].GetComponent<Bolo>().boloCaido = false;


        }
    }
}
