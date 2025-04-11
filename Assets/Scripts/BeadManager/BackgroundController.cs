using UnityEngine;

namespace BeadManager
{
    public class BackgroundController : MonoBehaviour
    {
        private Camera mainCamera;
        public FlexibleColorPicker fcp;

        void Start()
        {
            mainCamera = GameObject.FindAnyObjectByType<Camera>();
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
            fcp.color = mainCamera.backgroundColor;
        }
    }
}
