using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Test
{

public class CleanTest : MonoBehaviour
{
    [SerializeField]
    private int brushRadius;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    Texture2D maskBase;
    Material mat;
    Texture2D mask;
    RaycastHit raycastHit;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mask = new Texture2D(maskBase.width, maskBase.height);
        mask.SetPixels(maskBase.GetPixels());
        mask.Apply();
        mat.SetTexture("_MaskTex", mask);
    }

    // Update is called once per frame
    void Update()
    {
        // Step 1: Get mouse position in screen coordinates
        Vector3 mouseScreenPos = Input.mousePosition;

        // Step 2: Convert screen position to world coordinates
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);


        mouseWorldPos.z = spriteRenderer.transform.position.z;

        // Step 3: Check if the mouse is inside the sprite bounds
        if (spriteRenderer.bounds.Contains(mouseWorldPos))
        {
            // Step 4: Convert world coordinates to sprite local coordinates
            Vector3 mouseLocalPos = spriteRenderer.transform.InverseTransformPoint(mouseWorldPos);

            // Get the sprite bounds and texture dimensions
            Bounds spriteBounds = spriteRenderer.sprite.bounds;
            Vector2 textureSize = new Vector2(spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height);

            // Step 5: Convert local coordinates to texture coordinates
            float xCoord = (mouseLocalPos.x - spriteBounds.min.x) / spriteBounds.size.x;
            float yCoord = (mouseLocalPos.y - spriteBounds.min.y) / spriteBounds.size.y;

            // Ensure the coordinates are clamped between 0 and 1
            xCoord = Mathf.Clamp01(xCoord);
            yCoord = Mathf.Clamp01(yCoord);

            // Convert normalized coordinates to texture pixel coordinates
            Vector2 textureCoord = new Vector2(xCoord * textureSize.x, yCoord * textureSize.y);

            Vector2 coordOffset = new Vector2(textureCoord.x - brushRadius / 2, 
                                                textureCoord.y - brushRadius / 2);
            for(int i = 0; i < brushRadius; i++)
            {
                for(int j = 0; j < brushRadius; j++)
                {
                    mask.SetPixel((int)coordOffset.x + i, (int)coordOffset.y + j, Color.black);
                }
            }

            mask.Apply();
            // Debug.Log($"Texture Coordinates: {textureCoord}");

        }
    }
}

}

