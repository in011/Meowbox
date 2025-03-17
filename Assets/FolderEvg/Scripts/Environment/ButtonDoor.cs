using UnityEngine;
using UnityEngine.Rendering;

public class ButtonDoor : MonoBehaviour
{
    [SerializeField] private NewDoorScript door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Block")
        {
            door.Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Block")
        {
            door.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
