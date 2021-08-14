using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Character2 : MonoBehaviour
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
            transform.LookAt(nuevoCamino[current2].transform);
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
                for (int i = 0; i < path.Length; i++)
                {
                    float distancetoNode = Vector3.Distance(
                        player.transform.position,
                        path[i].transform.position
                        );
                    if (distancetoNode < enemyVision)
                    {
                        print("Detectado");

                        normal = false;
                        nuevoCamino = Breadthwise(path[current], path[i]).ToArray();
                    }
                }
                if (distance < threshold)
                {
                    current++;
                    current %= path.Length;
                }
                yield return new WaitForSeconds(0.18f);
            }
            else if (!normal)
            {
                float distance = Vector3.Distance(
                       transform.position,
                       nuevoCamino[current2].transform.position
                       );
                if (distance < threshold)
                {

                    // move to the next one
                    current2++;

                    // if out of bounds return to 0
                    //                  current2 %= path.Length;
                    if (current2 == nuevoCamino.Length && !deRegreso)
                    {
                        nuevoCamino = Breadthwise(nuevoCamino[nuevoCamino.Length - 1], path[current]).ToArray();
                        deRegreso = true;
                        current2 = 0;
                    }
                    else if (current2 == nuevoCamino.Length && deRegreso)
                    {
                        normal = true;
                        current2 = 0;
                        deRegreso = false;
                    }
                }


                yield return new WaitForSeconds(0.18f);
            }
        }
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
