using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public Node[] neighbors;

    // how do you know how you got here?
    public List<Node> history;

    public Node(Node[] neighbors)
    {
        this.neighbors = neighbors;
    }

    public float G
    {
        get;
        set;
    }

    public float H
    {
        get;
        set;
    }

    public float F
    {
        get
        {
            return G + H;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, new Vector3(1f,1f,1f));
        Gizmos.color = new Color(255,200,0);
        if (neighbors != null)
        {


            for (int i = 0; i < neighbors.Length; i++)
            {
                if (neighbors[i] == null)
                    continue;

                Gizmos.DrawLine(
                    transform.position,
                    neighbors[i].transform.position
                    );
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, 1);
    }

}
