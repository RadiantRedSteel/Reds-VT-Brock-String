using System.Collections.Generic;
using UnityEngine;

// WIP
namespace BeadManager
{
    public class BrockStringSystem : MonoBehaviour
    {
        private BeadProperties beadProperties;

        [SerializeField] private GameObject redBeadSpherePrefab;
        [SerializeField] private GameObject greenBeadSpherePrefab;
        [SerializeField] private GameObject yellowBeadSpherePrefab;

        //[SerializeField] private GameObject letterAFab;
        //[SerializeField] private GameObject letterBFab;
        //[SerializeField] private GameObject letterCFab;

        private GameObject selectedBead;

        private Dictionary<string, GameObject> beadShapes; // Use to map string identifiers to bead shapes

        void Start()
        {
            beadProperties = GetComponent<BeadProperties>();
            LoadBeadShapes(); // Load your bead shapes here  
        }

        void LoadBeadShapes()
        {
            // Initialize your shapes, e.g., with prefabs or GameObjects  
            beadShapes = new Dictionary<string, GameObject>
            {
                { "RedSphere", redBeadSpherePrefab },
                { "GreenSphere", greenBeadSpherePrefab },
                { "YellowSphere", yellowBeadSpherePrefab },
                { "Triangle", /* Your Triangle prefab */ null },
                { "Letter", /* Your Triangle prefab */ null }
                // Add more shapes as needed  
            };
        }

        public void ChangeBeadShape(string shapeName)
        {
            if (beadShapes.ContainsKey(shapeName))
            {
                // Destroy existing beads if needed  
                DestroyBeads();

                // Instantiate new beads based on the selected shape  
                var newShape = beadShapes[shapeName];

                // Clone or instantiate the new shape for each bead type  
                Instantiate(newShape, selectedBead.transform.localPosition, Quaternion.identity /* parent transform if needed */);
            }
        }

        private void UpdateBeadProperties(GameObject newShape)
        {
            // Depending on your design, set the new shapes to the appropriate properties  
            // For instance, if newShape is red, assign it to redBead in BeadProperties  
            if (newShape.name.Contains("Red"))
            {
                //beadProperties.RedBead = newShape;
            }
            // Repeat for other colors/letters...  
        }

        public void ChangeBeadColor(Color newColor)
        {
            foreach (var bead in new[] { beadProperties.RedBead, beadProperties.YellowBead, beadProperties.GreenBead })
            {
                if (bead != null)
                {
                    var renderer = bead.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = newColor;  // Change bead color  
                    }
                }
            }
        }

        private void DestroyBeads()
        {
            // If you want to remove existing bead GameObjects  
            foreach (var bead in new[] { beadProperties.RedBead, beadProperties.YellowBead, beadProperties.GreenBead })
            {
                if (bead != null)
                {
                    Destroy(bead);
                }
            }
        }
    }
}