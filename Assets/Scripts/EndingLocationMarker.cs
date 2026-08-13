using UnityEngine;

public class EndingLocationMarker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject endingLoc;
    public GameObject cam;
    public float offset = 500;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(endingLoc.transform.position.x, endingLoc.transform.position.y, cam.transform.position.z + offset);
    }
}
