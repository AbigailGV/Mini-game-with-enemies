using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 15, ForceMode2D.Impulse);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
