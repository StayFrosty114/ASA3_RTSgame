using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_UnitControl : MonoBehaviour
{
    public Camera mainCam;
    private bool selected = false;
    private NavMeshAgent agent;
    private Vector3 destination;
    private Color unitColor;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        Movement();

        if (selected == true)
            unitColor = Color.cyan;
        else if (selected == false)
            unitColor = Color.green;
    }

    // Unit movement and toggling selected units.
    private void Movement()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();

            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (renderer != null)
                    {
                        hit.collider.GetComponent<Renderer>().material.color = unitColor;
                        selected = !selected;
                        print(selected);
                    }
                }
                

            }

            if (Input.GetMouseButtonDown(1) && selected == true)
            {
                destination = hit.point;
                gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
            }

        }
    }
}
