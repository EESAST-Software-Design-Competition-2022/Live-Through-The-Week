using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.name.Equals("Player") && GameManager.instance.gameState == GAMESTATE.ON)
        {
            Time.timeScale = 0;
            GameManager.instance.gameState = GAMESTATE.SLEEP;
            UIManager.instance.showSleepUI(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.name.Equals("Player") && GameManager.instance.gameState == GAMESTATE.ON)
        {
            Time.timeScale = 0;
            GameManager.instance.gameState = GAMESTATE.SLEEP;
            UIManager.instance.showSleepUI(1);
        }
    }
}
