using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_UnitControl : MonoBehaviour
{
    // Component Variables
    public Camera mainCam;
    private NavMeshAgent agent;

    private Color unitColor;
    private bool selected = false;
    private float radius = 8f;
    private Vector3 destination;

    // LineRenderer Variables
    public LineRenderer attackLaser;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 8f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        attackLaser.enabled = false;
        attackLaser.startWidth = laserWidth;
    }
    
    void FixedUpdate()
    {
        // Setting Unit colour when unit is selected/deselected
        if (selected == true)
            unitColor = Color.cyan;
        else if (selected == false)
            unitColor = Color.green;

        Movement();
        VisionCheck();
    }

    #region Unit Movement
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
    #endregion


    // Draws a sphere to show vision radius and draws a line to nearby enemies.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<SC_EnemyTower>() || collider.gameObject.GetComponent<SC_EnemyUnit>())
            {
                Gizmos.DrawLine(collider.transform.position, transform.position);
            }
        }
    }

    // Checks for enemies in a radius using a raycast sphere and sets target destination if enemy is spotted.
    private void VisionCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<SC_EnemyTower>() || collider.gameObject.GetComponent<SC_EnemyUnit>())
            {
                destination = (collider.transform.position);
                gameObject.GetComponent<NavMeshAgent>().SetDestination(destination - new Vector3(0,0,2));
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.GetComponent<SC_EnemyTower>() || hit.collider.gameObject.GetComponent<SC_EnemyUnit>())
            {
                // Draws a line to show that the unit is attacking.
                Vector3 direction = hit.collider.transform.position - transform.position;
                ShootLaser(transform.position, direction, laserMaxLength);
                attackLaser.enabled = true;
            }
        }
        yield return new WaitForSeconds(1);
    }

    // Renders a raycast line to show where the unit is attacking.
    void ShootLaser(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit laserHit;
        Vector3 endPosition = targetPosition + (direction);

        if (Physics.Raycast(ray, out laserHit, length))
        {
            if (laserHit.collider.gameObject.GetComponent<SC_EnemyTower>() || laserHit.collider.gameObject.GetComponent<SC_EnemyUnit>())
                endPosition = laserHit.collider.transform.position;
        }

        attackLaser.SetPosition(0, targetPosition);
        attackLaser.SetPosition(1, endPosition);
    }
}
