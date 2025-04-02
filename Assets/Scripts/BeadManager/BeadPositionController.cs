using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeadPositionController : MonoBehaviour
{
    public Slider redSlider;
    public Slider yellowSlider;
    public Slider greenSlider;

    public GameObject redBead;
    public GameObject yellowBead;
    public GameObject greenBead;
    
    void Start()
    {
        // Set slider values based on the local Y position of the beads  
        redSlider.value = redBead.transform.localPosition.y;
        yellowSlider.value = yellowBead.transform.localPosition.y;
        greenSlider.value = greenBead.transform.localPosition.y;
    }

    void Update()
    {
        // Update the local Y position of each bead based on the respective slider value  
        UpdateBeadLocalPosition(redBead, redSlider);
        UpdateBeadLocalPosition(yellowBead, yellowSlider);
        UpdateBeadLocalPosition(greenBead, greenSlider);
    }

    //private void UpdateBeadPosition(GameObject bead, Slider slider)
    //{
    //    Vector3 newPosition = bead.transform.position;
    //    newPosition.y = slider.value; // Update only the y component  
    //    bead.transform.position = newPosition; // Set the new position  
    //}

    private void UpdateBeadLocalPosition(GameObject bead, Slider slider)
    {
        Vector3 newLocalPosition = bead.transform.localPosition;
        newLocalPosition.y = slider.value; // Update the local Y component  
        bead.transform.localPosition = newLocalPosition; // Set the new local position  
    }
}
