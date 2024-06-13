using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC.Hunting
{

    public class Deer : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float moveForwardDistance;
        [SerializeField]
        private float moveForwardTime;
        void Update()
        {
            if (HuntingManager.instance.MainAjag.CurrentStamina <= 0)
            {
                return;
            }
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        public IEnumerator MoveForward()
        {
            float elapsedTime = 0;
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + Vector3.right * moveForwardDistance;
            while (elapsedTime < moveForwardTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveForwardTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = endPos;
        }

        public void MoveBackward(float ammount)
        {
            transform.position += Vector3.left * ammount;
        }

    }

}

