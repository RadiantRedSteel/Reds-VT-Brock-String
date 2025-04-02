using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BeadBlinker : MonoBehaviour
{
    public TMP_Dropdown BeadSelectDropdown;
    public Slider BeadBlinkSpeedSlider;
    public Button ToggleBlinkButton;

    public GameObject redBead;
    public GameObject yellowBead;
    public GameObject greenBead;
    GameObject bead;

    private Material beadMaterial; // Reference to the bead material  
    private Color originalColor; // Original color of the bead  
    private Coroutine blinkingCoroutine; // Coroutine for blinking 

    private bool isBlinking = false;
    private float blinkSpeed;
    private float targetBlinkSpeed = 0.5f;
    private float currentBlinkSpeed = 0.5f; // Start with the default blink speed  

    void Start()
    {
        // Initialize with the first bead
        blinkSpeed = BeadBlinkSpeedSlider.value;
        bead = redBead;
        ChooseBead();
        UpdateMaterial();

        // Attach the OnValueChanged listener to the slider
        BeadBlinkSpeedSlider.onValueChanged.AddListener(OnBlinkSpeedChanged);
    }


    public void ChooseBead()
    {
        switch (BeadSelectDropdown.value)
        {
            case 0:
                bead = redBead;
                break;
            case 1:
                bead = yellowBead;
                break;
            case 2:
                bead = greenBead;
                break;
        }
        // Update the material and original color based on the selected bead
        Debug.Log($"Selected Bead: {bead.name}"); // Log the selected bead  
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        beadMaterial = bead.GetComponent<Renderer>().material;
        originalColor = beadMaterial.color;
    }

    public void BlinkControl()
    {
        if (isBlinking == false)
        {
            StartBlinking(targetBlinkSpeed);
            isBlinking = true;
            Debug.Log(targetBlinkSpeed);
        }
        else
        {
            StopBlinking();
            isBlinking = false;
        }
    }

    private void OnBlinkSpeedChanged(float newSpeed)
    {
        targetBlinkSpeed = newSpeed; // Set the target speed  
        // If blinking is active, update the current speed smoothly  
        if (isBlinking)
        {
            // Optional: Slow down transitions  
            currentBlinkSpeed = Mathf.Lerp(currentBlinkSpeed, targetBlinkSpeed, Time.deltaTime);
        }
    }

    private void StartBlinking(float blinkSpeed)
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine); // Stop previous blinking  
        }
        blinkingCoroutine = StartCoroutine(Blink(blinkSpeed));
    }

    private IEnumerator Blink(float speed)
    {
        float elapsedTime = 0f;

        while (true)
        {
            // Smoothly transition to target speed  
            currentBlinkSpeed = Mathf.Lerp(currentBlinkSpeed, targetBlinkSpeed, Time.deltaTime * 5f); // Adjust 5f for speed of transition  
            elapsedTime += Time.deltaTime * currentBlinkSpeed;

            // Oscillate between original and brighter color  
            float lerpValue = Mathf.PingPong(elapsedTime, 1f);
            beadMaterial.color = Color.Lerp(originalColor, Color.white, lerpValue);

            yield return null; // Wait for the next frame  
        }
    }


    private void StopBlinking()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            beadMaterial.color = originalColor; // Reset to original color  
        }
    }
}
