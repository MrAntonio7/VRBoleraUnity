using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolo : MonoBehaviour
{
    public Vector3 posicionIniPos;
    public Quaternion posicionIniRot;
    private AudioSource AudioBolo;
    public bool boloCaido = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioBolo = GetComponent<AudioSource>();
        posicionIniPos = transform.position;
        posicionIniRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles);
        //Debug.Log(transform.up.y);
    }
    public void DestruyeBolo()
    {
        Destroy(gameObject);
    }
    public void DesactivaBolo()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bola" || collision.gameObject.tag == "Bolos")
        {
            AudioBolo.Play();
        };
    }
}
