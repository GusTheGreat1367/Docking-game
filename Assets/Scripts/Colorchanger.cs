using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Allows you to set a custom color directly in the Unity Inspector
    public Color customColor = Color.red; 

    void Start()
    {
        // Accesses the MeshRenderer and updates the material's color property
        GetComponent<MeshRenderer>().material.color = customColor;
    }
}
