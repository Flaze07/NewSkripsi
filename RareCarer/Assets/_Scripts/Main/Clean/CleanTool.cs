using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RC
{

public class CleanTool : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;
    [NonSerialized]
    public Vector2 Offset;
    [SerializeField]
    private float cleanPercentage = 10;
    private Canvas canvas;
    private Animal animal;
    private SpriteRenderer animalSpriteRenderer;
    private Vector3 prevMousePos;
    public void Initialize(Canvas canvas)
    {
        this.canvas = canvas;
        prevMousePos = Input.mousePosition;
    }
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
        UpdatePosition();
        CleanAnimal();
    }

    private void UpdatePosition()
    {
        var mousePos = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, 
                                                                                Input.mousePosition, canvas.worldCamera, 
                                                                                out Vector2 pos);
        var offseted = pos + offset;
        transform.position = canvas.transform.TransformPoint(offseted);
    }

    private void CleanAnimal()
    {
        if(Input.mousePosition == prevMousePos)
        {
            return;
        }
        if(animal == null || animalSpriteRenderer == null)
        {
            animal = GameManager.instance.CurrentAnimal;
            animalSpriteRenderer = animal.Sprite;
        }

        var mouseScreenPos = Input.mousePosition;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        var prevWorldPos = Camera.main.ScreenToWorldPoint(prevMousePos);
        mouseWorldPos.z = animalSpriteRenderer.transform.position.z;

        if(animalSpriteRenderer.bounds.Contains(mouseWorldPos))
        {
            Vector2 textureSize = new Vector2(animalSpriteRenderer.sprite.texture.width, animalSpriteRenderer.sprite.texture.height);
            Bounds spriteBounds = animalSpriteRenderer.sprite.bounds;

            Vector3 currentLocalPos = animalSpriteRenderer.transform.InverseTransformPoint(mouseWorldPos);
            float currentX = (currentLocalPos.x - spriteBounds.min.x) / spriteBounds.size.x;
            float currentY = (currentLocalPos.y - spriteBounds.min.y) / spriteBounds.size.y;

            Vector3 prevLocalPos = animalSpriteRenderer.transform.InverseTransformPoint(prevWorldPos);
            float prevX = (prevLocalPos.x - spriteBounds.min.x) / spriteBounds.size.x;
            float prevY = (prevLocalPos.y - spriteBounds.min.y) / spriteBounds.size.y;

            var diff = new Vector2(currentX - prevX, currentY - prevY);
            var diffScalar = diff.magnitude;
            var cleanPercent = diffScalar * cleanPercentage;
            animal.Cleanliness = Mathf.Clamp(animal.Cleanliness + cleanPercent, 0, 110);
            
        }
        prevMousePos = Input.mousePosition;
    }
}

}

