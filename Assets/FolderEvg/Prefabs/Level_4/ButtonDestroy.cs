using UnityEngine;

public class ButtonDestroy : MonoBehaviour
{
    [SerializeField] private GameObject[] _destroyObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Debug.Log("Broken");
            foreach(GameObject obj in _destroyObject)
            {
                obj.SetActive(false);
            }
        }
    }
}
