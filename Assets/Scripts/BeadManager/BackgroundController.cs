using UnityEngine;

namespace BeadManager
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private FlexibleColorPicker fcp;

        void Start()
        {
            fcp.color = mainCamera.backgroundColor;
            fcp.onColorChange.AddListener(OnFCPColorChanged);
        }

        private void OnFCPColorChanged(Color newColor)
        {
            mainCamera.backgroundColor = newColor;
        }

        public void SyncBackgroundColor()
        {
            // This is to ensure that upon loading color settings that fcp is also updated
            // TODO: This doesn't work - only loading the scene again seems to set the starting color properly
            fcp.color = mainCamera.backgroundColor;
            fcp.SetColor(mainCamera.backgroundColor);
        }
    }
}
