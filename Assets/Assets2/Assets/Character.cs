using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Node[] path;
    public Node[] everyNode;
    public GameObject player;

    public float speed = 5;
    public float threshold; // umbral
    public float enemyVision; // umbral del enemigo

    public GameObject nodo;
    public bool normal = true;
    public Node[] nuevoCamino;
    public bool deRegreso;

    public int current;
    public int current2;

    void Start()
    {
        current = 0;
        StartCoroutine(DistanceCheck());
    }

    void Update()
    {
        if (normal)
        {
            transform.LookAt(path[current].transform);
            transform.Translate(speed * transform.forward * Time.deltaTime, Space.World);
        }
        else
        {
            StopCoroutine(DistanceCheck());
            transform.LookAt(player.transform);
            transform.Translate(speed * transform.forward * Time.deltaTime, Space.World);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Respawn")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DistanceCheck()
    {

        while (true)
        {
            if (normal)
            {
                float distance = Vector3.Distance(
                    transform.position,
                    path[current].transform.position
                    );
                float distancetoPlayer = Vector3.Distance(
                    player.transform.position,
                    path[current].transform.position
                    );
                if (distancetoPlayer < enemyVision)
                {
                    print("Detectado");
                    normal = false;
                }
                else if (distance < threshold)
                {
                    current++;
                    current %= path.Length;
                }
                yield return new WaitForSeconds(0.18f);
            }
            else if (!normal)
            {
                float distancetoPlayer = Vector3.Distance(
                                    player.transform.position,
                                    path[current].transform.position
                                    );
                
                if (distancetoPlayer < threshold)
                {
                    print("Estoy con el jugador");
                }

                yield return new WaitForSeconds(0.18f);
            }
        }
    }

    private void OnMouseDown()
    {
        print("CLICK!");
        float min = 99999999999;
        int nodoMasCercano = 0;
        for (int i = 0; i < everyNode.Length; i++)
        {
            float distances = Vector3.Distance(
                this.transform.position,
                everyNode[i].transform.position
                );
            if (distances < min)
            {
                min = distances;
                nodoMasCercano = i;
            }
        }
        print(everyNode[nodoMasCercano]);

        nuevoCamino = Breadthwise(path[current], everyNode[nodoMasCercano]).ToArray();
        if (path[current] == nuevoCamino[1])
        {
            /* Node[] caminoAux= new Node[nuevoCamino.Length-1];
             Array.Copy(nuevoCamino, 1, caminoAux, 0, caminoAux.Length);
             nuevoCamino = caminoAux; */
            current2++;
        }
        normal = false;
        //      Instantiate(nodo,transform.position, Quaternion.Euler(0f,0f,0f));
    }

    public static List<Node> Breadthwise(Node start, Node end)
    {

        Queue<Node> work = new Queue<Node>();
        List<Node> visited = new List<Node>();

        start.history = new List<Node>();

        work.Enqueue(start);
        visited.Add(start);

        while (work.Count > 0)
        {
            Node current = work.Dequeue();

            if (current == end)
            {
                List<Node> result = new List<Node>(current.history);
                result.Add(current);
                return result;
            }
            else
            {
                for (int i = 0; i < current.neighbors.Length; i++)
                {
                    Node child = current.neighbors[i];

                    if (visited.Contains(child) == false)
                    {
                        child.history = new List<Node>(current.history);
                        child.history.Add(current);

                        work.Enqueue(child);
                        visited.Add(child);
                    }
                }
            }
        }
        return null;
    }
}
