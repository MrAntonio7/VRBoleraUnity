using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CanvasBola : MonoBehaviour
{

    public Camera mainCamera;
    public Canvas canvas;
    public XRBaseInteractor rHand;
    public XRBaseInteractor lHand;
    private AudioSource AudioBola;
    // Start is called before the first frame update
    void Start()
    {
        AudioBola = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            canvas.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);



        }

        
    }
   //transform.up.y

    public void mostrarInfo()
    {
        if (!rHand.hasSelection && !lHand.hasSelection)
        {
            canvas.gameObject.SetActive(false);
        }
    }
    public void ocultarInfo()
    {
        if (rHand.hasSelection && lHand.hasSelection)
        {
            canvas.gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            AudioBola.Play();
        }
    }
}
