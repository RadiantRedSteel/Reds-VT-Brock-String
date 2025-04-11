using UnityEngine;

namespace BeadManager
{
    public class BeadProperties : MonoBehaviour
    {
        [SerializeField] private GameObject redBead;
        [SerializeField] private GameObject yellowBead;
        [SerializeField] private GameObject greenBead;

        public GameObject RedBead => redBead;
        public GameObject YellowBead => yellowBead;
        public GameObject GreenBead => greenBead;

        public Vector3 RedBeadPosition
        {
            get => redBead.transform.localPosition;
            set
            {
                if (redBead != null)
                {
                    redBead.transform.localPosition = value;
                }
            }
        }

        public Vector3 YellowBeadPosition
        {
            get => yellowBead.transform.localPosition;
            set
            {
                if (yellowBead != null)
                {
                    yellowBead.transform.localPosition = value;
                }
            }
        }

        public Vector3 GreenBeadPosition
        {
            get => greenBead.transform.localPosition;
            set
            {
                if (greenBead != null)
                {
                    greenBead.transform.localPosition = value;
                }
            }
        }

        //public void SetBeadShape()

        void Start()
        {
            if (!redBead || !yellowBead || !greenBead)
            {
                Debug.LogWarning("One or more bead GameObjects are not assigned in the BeadProperties script.", this);
            }
        }

        public (GameObject red, GameObject yellow, GameObject green) GetBeads()
        {
            return (redBead, yellowBead, greenBead);
        }
    }
}
