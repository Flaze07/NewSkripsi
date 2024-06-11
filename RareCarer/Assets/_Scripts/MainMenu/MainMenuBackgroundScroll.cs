using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.MainMenu
{
    public class MainMenuBackgroundScroll : MonoBehaviour
    {
        [SerializeField] private Material BGMaterial;
        [SerializeField] private float minDistance = 0f;
        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private float duration = 10f;

        private void Start()
        {
            StartCoroutine(MovementLoop());
        }

        private IEnumerator MovementLoop()
        {
            while (true)
            {
                yield return StartCoroutine(MoveToPosition(new Vector2(
                BGMaterial.mainTextureOffset.x + Random.Range(minDistance, maxDistance),
                BGMaterial.mainTextureOffset.y + Random.Range(minDistance, maxDistance)
                ), duration));
            }
        }

        private IEnumerator MoveToPosition(Vector2 pos, float duration)
        {
            //https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
            float distance = Vector2.Distance(pos, BGMaterial.mainTextureOffset);
            float speed = distance/duration;

            float startTime = Time.time;
            Vector2 initialPos = BGMaterial.mainTextureOffset;

            float progress = 1f;

            do
            {
                // Distance moved equals elapsed time times speed..
                float distanceCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed equals current distance divided by total distance.
                progress = distanceCovered / distance;

                // Set our position as a fraction of the distance between the markers.
                BGMaterial.mainTextureOffset = Vector2.Lerp(initialPos, pos, progress);

                yield return new WaitForEndOfFrame();
            } while (progress < 1f);
        }
    }
}
