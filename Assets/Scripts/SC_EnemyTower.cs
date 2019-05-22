using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EnemyTower : MonoBehaviour
{
    
    public float maxHealth = 500;
    private float currentHealth;
    private float damage = 0.5f;
    private Color healthColor;
    private Color damagedColor = new Color(10f, 200f, 0f, 255f);

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        GetComponent<Renderer>().material.color = healthColor;

        if (currentHealth <= (maxHealth / 2))
        {
            GetComponent<Renderer>().material.color = damagedColor;
            Debug.Log("Ow");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<SC_UnitControl>())
        {
            currentHealth -= damage;
            Debug.Log(currentHealth);
        }
    }

    private void Death()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }
}
