using UnityEngine;
using UnityEngine.SceneManagement;


public class SC_Overlord : MonoBehaviour
{
    // Variables
    public GameObject bossTower;
   // public int enemyScore = 0;

    // UI Variables
    public GameObject winPopUp;
    public GameObject lossPopUp;
    

    void Start()
    {
        // Hiding UI Popups at start.
        winPopUp.SetActive(false);
        lossPopUp.SetActive(false);
    }
    
    void Update()
    {
        // If the boss tower is destroyed, the player wins.
        if (bossTower == null)
        {
            Victory();
        }
        
        // If the enemy destroys all 5 friendly units, the player loses.
       // if (enemyScore == 5)
       // {
       //     Defeat();
       // }
    }

    private void Victory()
    {
        winPopUp.SetActive(true);
    }

    private void Defeat()
    {
        lossPopUp.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
