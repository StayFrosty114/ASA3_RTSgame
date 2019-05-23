using UnityEngine;

public class SC_EnemyTower : MonoBehaviour
{
    
    public float maxHealth = 500;
    private float currentHealth;
    private float damage = 0.5f;
    private Color healthColor = Color.red;
    private Color damagedColor = new Color(10f, 200f, 0f, 255f);

    // Start is called before the first frame update
    void Start()
    {
        // Sets the Tower's current health to maximum at start.
        currentHealth = maxHealth;

        // Sets the material color to the healthColor value.
        GetComponent<Renderer>().material.color = healthColor;

        // Changes the Tower's color when it drops below half HP to indicate damage.
        if (currentHealth <= (maxHealth / 2))
        {
            GetComponent<Renderer>().material.color = damagedColor;
            Debug.Log("Ow");
        }

    }

    
    void Update()
    {
        // Checks if the Tower's health drops below 0 and calls a Death function if true
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // While a friendly unit is in range, the tower takes damage.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_UnitControl>())
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
        }
    }

    // Destroys the tower when health reaches (or drops below) 0.
    private void Death()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }
}
