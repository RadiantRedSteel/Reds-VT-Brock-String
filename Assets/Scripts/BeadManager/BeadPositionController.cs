using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeadManager
{
    [RequireComponent(typeof(BeadProperties))]
    public class BeadPositionController : MonoBehaviour
    {
        [SerializeField] private Slider redSlider;
        [SerializeField] private Slider yellowSlider;
        [SerializeField] private Slider greenSlider;

        private BeadProperties bP;

        void Start()
        {
            // Assuming BeadProperties is on the same GameObject, otherwise find the GameObject  
            bP = GetComponent<BeadProperties>();

            // Initialize the sliders based on the bead properties  
            UpdateBeadsOnShapeChange();

            redSlider.onValueChanged.AddListener(OnBeadDistanceChanged);
            yellowSlider.onValueChanged.AddListener(OnBeadDistanceChanged);
            greenSlider.onValueChanged.AddListener(OnBeadDistanceChanged);
        }

        private void OnBeadDistanceChanged(float newDistance)
        {
            UpdateBeadLocalPosition(bP.RedBead, redSlider);
            UpdateBeadLocalPosition(bP.YellowBead, yellowSlider);
            UpdateBeadLocalPosition(bP.GreenBead, greenSlider);
        }

        private void UpdateBeadLocalPosition(GameObject bead, Slider slider)
        {
            if (bead != null)
            {
                Vector3 newLocalPosition = bead.transform.localPosition;
                newLocalPosition.y = slider.value; // Update the local Y component  
                bead.transform.localPosition = newLocalPosition; // Set the new local position  
            }
            else
            {
                Debug.LogError("Bead GameObject is null.");
            }
        }

        public void UpdateBeadsOnShapeChange()
        {
            if (bP != null)
            {
                redSlider.value = bP.RedBead.transform.localPosition.y;
                yellowSlider.value = bP.YellowBead.transform.localPosition.y;
                greenSlider.value = bP.GreenBead.transform.localPosition.y;
            }
        }
    }
}