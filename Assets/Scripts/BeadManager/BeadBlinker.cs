using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeadManager
{
    [RequireComponent(typeof(BeadProperties))]
    public class BeadBlinker : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown BeadSelectDropdown;
        [SerializeField] private Slider BeadBlinkSpeedSlider;
        [SerializeField] private Button ToggleBlinkButton;

        private GameObject bead;
        private BeadProperties bP;

        private Material beadMaterial; // Reference to the bead material  
        private Color originalColor; // Original color of the bead  
        private Coroutine blinkingCoroutine; // Coroutine for blinking 

        private bool isBlinking = false;
        private float targetBlinkSpeed;
        private float currentBlinkSpeed;

        void Start()
        {
            // Get the BeadProperties component attached to this GameObject  
            bP = GetComponent<BeadProperties>();

            // Initial setup for the bead  
            currentBlinkSpeed = BeadBlinkSpeedSlider.value;
            targetBlinkSpeed = BeadBlinkSpeedSlider.value;
            bead = bP.RedBead;
            ChooseBead();
            UpdateMaterial();

            // Attach the OnValueChanged listener to the slider
            BeadBlinkSpeedSlider.onValueChanged.AddListener(OnBlinkSpeedChanged);
        }

        public void ChooseBead()
        {
            // Stop any ongoing blinking before changing the bead  
            StopBlinking();

            // Choose the bead based on dropdown selection using BeadProperties  
            switch (BeadSelectDropdown.value)
            {
                case 0:
                    bead = bP.RedBead;
                    break;
                case 1:
                    bead = bP.YellowBead;
                    break;
                case 2:
                    bead = bP.GreenBead;
                    break;
                default:
                    Debug.LogWarning("Invalid bead selection.");
                    return;
            }

            // Update the material and original color based on the selected bead
            Debug.Log($"Selected Bead: {bead.name}"); // Log the selected bead  
            UpdateMaterial();
        }

        private void UpdateMaterial()
        {
            if (bead != null)
            {
                beadMaterial = bead.GetComponent<Renderer>().material;
                originalColor = beadMaterial.color;
            }
        }

        public void BlinkControl()
        {
            if (isBlinking == false)
            {
                StartBlinking();
                Debug.Log(targetBlinkSpeed);
            }
            else
            {
                StopBlinking();
            }
        }

        private void OnBlinkSpeedChanged(float newSpeed)
        {
            targetBlinkSpeed = newSpeed; // Set the target speed  
            
            // If blinking is active, update the current speed smoothly  
            if (isBlinking)
            {
                // Smoothly transition to new blinking speed   
                currentBlinkSpeed = Mathf.Lerp(currentBlinkSpeed, targetBlinkSpeed, Time.deltaTime);
            }
        }

        private void StartBlinking()
        {
            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine); // Stop previous blinking  
            }

            if (bead == null || !bead.activeInHierarchy) // Check if the bead is still valid  
            {
                Debug.LogWarning("Selected bead is not valid for blinking.");
                return;
            }

            blinkingCoroutine = StartCoroutine(Blink());
            isBlinking = true;
        }

        private IEnumerator Blink()
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
                isBlinking = false;
            }
        }
    }
}