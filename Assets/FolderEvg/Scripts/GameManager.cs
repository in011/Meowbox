using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    private Moving player1Script;
    private bool player1Dead = false;
    [SerializeField] private GameObject player2;
    private Moving player2Script;
    private bool player2Dead = false;

    [SerializeField] private GameObject[] blockPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        player1Script = player1.GetComponent<Moving>();
        player2Script = player2.GetComponent<Moving>();

        Invoke(nameof(DropBlock), 3); // ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        // Test button
        if (Input.GetButtonDown("Fire1"))
        {
            //Player1Death();
        }
    }

    public void Player1Death()
    {
        player1Dead = true;
        if (player2Dead)
        {
            Debug.Log("GAME OVER!");
        }

        player1Script.Death();
        player1.transform.position = player1Script.safePos;
        player1.SetActive(false);
        Invoke(nameof(RespawnPlayer1), 6); // �������� ����� ResetJump ����� jumpCooldown ������
        StartCoroutine(Timer(5));
    }
    public void Player2Death()
    {
        player2Dead = true;
        if (player1Dead)
        {
            Debug.Log("GAME OVER!");
        }

        player2Script.Death();
        player2.transform.position = player2Script.safePos;
        player2.SetActive(false);
        Invoke(nameof(RespawnPlayer2), 6); // �������� ����� ResetJump ����� jumpCooldown ������
        StartCoroutine(Timer(5));
    }

    private void RespawnPlayer1()
    {
        player1Dead = false;
        Debug.Log("Revived Cat!");
        player1.SetActive(true);
        player1Script.Revive();
    }
    private void RespawnPlayer2()
    {
        player2Dead = false;
        Debug.Log("Revived Cat!");
        player2.SetActive(true);
        player2Script.Revive();
    }

    // ������ ������� (�������)
    IEnumerator Timer(int seconds)
    {
        do
        {
            --seconds;
            Debug.Log(seconds + "...");
            yield return new WaitForSeconds(1.0f);
        } while (seconds > 0);
    }

    private void DropBlock()
    {
        int randXPos = Random.Range(-7, 5);
        int randZPos = Random.Range(-7, 5);

        if(randXPos % 2 == 0)
        {
            randXPos -= 1;
        }
        if (randZPos % 2 == 0)
        {
            randZPos -= 1;
        }

        Instantiate(blockPrefabs[0], new Vector3((float)randXPos, 20f, (float)randZPos), blockPrefabs[0].transform.rotation);

        Invoke(nameof(DropBlock), 3); // �������� ��������� ����
    }
}