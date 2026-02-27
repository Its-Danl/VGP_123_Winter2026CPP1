using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] pickupPrefabs;
    void Start()
    {
        int randNumber = Random.Range(0,pickupPrefabs.Length);

        if (pickupPrefabs[randNumber] != null)
            Instantiate(pickupPrefabs[randNumber], transform.position, Quaternion.identity);
    }
}
