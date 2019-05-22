using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_EnemyUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private float radius = 5f;

    public float maxHealth = 10;
    private float currentHealth;
    private float damage = 0.5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        VisionCheck();

        if (currentHealth <= 0)
        {
            Death();
        }
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
                currentHealth -= damage;
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


    private void Death()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

}
