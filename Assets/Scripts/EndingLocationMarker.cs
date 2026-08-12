using UnityEngine;

public class EndingLocationMarker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform marker;
    public GameObject endingLoc;
    void Start()
    {
        marker = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        marker.anchoredPosition = new Vector2(endingLoc.transform.position.x, endingLoc.transform.position.y);
    }
}
