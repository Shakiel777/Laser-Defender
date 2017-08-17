using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Lives : MonoBehaviour {

    public int ships;
    private Text myShip;

    void Start()
    {
        myShip = GetComponent<Text>();
        Reset();
    }

    public void Ships(int points)
    {
        ships -= points;
        myShip.text = ships.ToString();
        if (ships <= 0)
        {
            Debug.Log("Load Lose Screen");
            SceneManager.LoadScene("Lose");
        }
    }

    // Update is called once per frame
    public void Reset()
    {
        ships = 3;
        myShip.text = ships.ToString();
    }
}
