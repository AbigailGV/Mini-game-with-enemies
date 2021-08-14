using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float speed;
    public GameObject bala;
    private Rigidbody2D rb;
    public GameObject pivote;
    private bool saltar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        float mover = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * Time.deltaTime * mover);
        if (Input.GetKeyDown(KeyCode.Space) && saltar)
        {
            rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            saltar = false;
        }

        Vector2 posicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicion = posicion - (Vector2)pivote.transform.position;
        posicion = posicion.normalized;
        pivote.transform.up = posicion;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(shoot());
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Finish")
        {
            saltar = true;
        }
    }

    IEnumerator shoot()
    {
        Instantiate(bala, pivote.transform.position, pivote.transform.rotation);
        yield return new WaitForSeconds(0.8f);
    }
}
