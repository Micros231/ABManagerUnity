using UnityEngine;

namespace VRProjectImitation.Characters
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField]
        private Texture2D _customTextureCrosshair;

        [SerializeField]
        private Vector2 _sizeCrosshair;
        private Texture2D _texturCrosshair;


        private void Awake()
        {
            if (_customTextureCrosshair != null)
                _texturCrosshair = _customTextureCrosshair;
            else
                _texturCrosshair = GetDotTexture();
        }

        private void OnGUI()
        {
            Vector2 crosshairPosition = new Vector2(Screen.width / 2, Screen.height / 2);
            GUI.DrawTexture(new Rect(crosshairPosition, _sizeCrosshair), _texturCrosshair);
        }

        private Texture2D GetDotTexture()
        {
            int sizeTexture = 64;
            Texture2D dotTexture = new Texture2D(sizeTexture, sizeTexture);
            int paddindSize = 5;
            for (int x = 0; x < dotTexture.width; x++)
            {
                for (int y = 0; y < dotTexture.height; y++)
                {
                    Color currentPixelColor = new Color(0, 0, 0, 1);
                    if (x >= dotTexture.width / 2 - paddindSize && x <= dotTexture.width / 2 + paddindSize &&
                        y >= dotTexture.height / 2 - paddindSize && y <= dotTexture.width / 2 + paddindSize)
                    {
                        currentPixelColor = new Color(0, 0, 0, 1);
                    }
                    dotTexture.SetPixel(x, y, currentPixelColor);
                }
            }
            dotTexture.Apply();
            return dotTexture;
        }
    }
}

