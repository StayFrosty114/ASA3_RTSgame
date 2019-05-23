using UnityEngine;
using UnityEngine.AI;

public class SC_EnemyUnit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private float radius = 5f;

    private float maxHealth = 100;
    private float currentHealth;
    private float damage = 0.5f;

   // Doesn't work, tried a few tactics, this was the one that came closest.
    // private SC_Overlord overLord;

    void Start()
    {
        // Capture a reference to the NavMeshAgent.
        agent = GetComponent<NavMeshAgent>();

        // Sets the unit's current health to maximum at start.
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Updates VisionCheck every frame.
        VisionCheck();

        // Calls the Death function when unit drops below 0 HP.
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // Checks for friendly units and targets any that get close. Friendly units damage the kamikaze while being targeted.
    private void VisionCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<SC_UnitControl>())
            {
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

    // If the unit collides with a friendly unit, it destroys itself and the collided unit.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_UnitControl>())
        {
           // overLord.enemyScore++;
           // Debug.Log(overLord.enemyScore);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    // Called if unit's current health drops below 0.
    private void Death()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

}
