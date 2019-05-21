using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_EnemyUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private float radius = 5f;

    public LineRenderer attackLaser;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        attackLaser.enabled = false;
        attackLaser.SetWidth(laserWidth, laserWidth);

    }

    // Update is called once per frame
    void Update()
    {
        VisionCheck();
    }

    private void VisionCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<SC_UnitControl>())
            {
                Debug.Log("FIRE IN THE HOLE");
                destination = (collider.transform.position);
                gameObject.GetComponent<NavMeshAgent>().SetDestination(destination);
                ShootLaser(transform.position, destination, laserMaxLength);
                attackLaser.enabled = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_UnitControl>())
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void ShootLaser(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit Hit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out Hit, length))
        {
            if (Hit.collider.gameObject.GetComponent<SC_EnemyTower>())
                endPosition = Hit.point;
        }

        attackLaser.SetPosition(0, targetPosition);
        attackLaser.SetPosition(1, endPosition);
    }

 
}
