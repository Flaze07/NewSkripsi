using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Old
{

public class AnimalCleanliness : MonoBehaviour
{
    [SerializeField]
    private Animal animal;
    [SerializeField]
    private Texture2D maskTexBase;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Material mat;
    private Texture2D maskTex;
    public int internalCleanliness;
    // Start is called before the first frame update
    void Start()
    {
        maskTex = new(maskTexBase.width, maskTexBase.height);
        maskTex.SetPixels(maskTexBase.GetPixels());
        maskTex.Apply();
        mat.SetTexture("_MaskTex", maskTex);
        internalCleanliness = Mathf.CeilToInt(animal.Cleanliness) + 1;
    }

    // Update is called once per frame
    public void UpdateCleanliness()
    {
        if(internalCleanliness != Mathf.CeilToInt(animal.Cleanliness))
        {
            var totalHeight = maskTex.height;
            var percentage = (100 - animal.Cleanliness) / 100;
            var height = Mathf.CeilToInt(totalHeight * percentage);
            for(int i = 0; i < height; ++i)
            {
                for(int j = 0; j < maskTex.width; ++j)
                {
                    maskTex.SetPixel(j, i, Color.black);
                }
            }
            maskTex.Apply();
            internalCleanliness = Mathf.CeilToInt(animal.Cleanliness);
        }
    }
    public void CleanAnimal(int x, int y)
    {
        maskTex.SetPixel(x, y, maskTexBase.GetPixel(x, y));
    }

    public void ApplyCLean()
    {
        maskTex.Apply();
        // for(int i = 0; i < maskTex.width; ++i)
        // {
        //     for(int j = 0; j < maskTex.height; ++j)
        //     {

        //     }
        // }
    }

    // private void CalculateCleanPercentage()
    // {
    //     for(int i = 0; i < maskTex.width; ++i)
    //     {
    //         for(int j = 0; j < maskTex.height; ++j)
    //         {

    //         }
    //     }
    // }
}

}

