using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1Death()
    {
        Invoke(nameof(RespawnPlayer1), 6); // вызывает метод ResetJump через jumpCooldown секунд
        StartCoroutine(Timer(5));
    }

    IEnumerator Timer(int seconds)
    {
        do
        {
            --seconds;
            Debug.Log(seconds + "...");
            yield return new WaitForSeconds(1.0f);
        } while (seconds > 0);
    }

    private void RespawnPlayer1()
    {
        Debug.Log("Burned Cat!");
    }
}
