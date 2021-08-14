using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy : MonoBehaviour

    
{

    public GameObject gameob;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(gameob.transform.position.x, gameob.transform.position.y,0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Respawn")
        {
            Destroy(gameob.gameObject);
            Destroy(this.gameObject);
        }
    }
}
