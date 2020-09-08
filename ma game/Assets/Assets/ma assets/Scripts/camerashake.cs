using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MaGame
{
    public class camerashake : MonoBehaviour
    {
        private Vector3 ogPosition;
        private bool _startCoroutine = false;
        void Start()
        {
            ogPosition = transform.localPosition;
            //if(Input.GetKeyDown(KeyCode ))
            //StartCoroutine(shakeRoutine(5.5f, 7f, 0.01f));
        }

        void Update()
        {

        }

        IEnumerator shakeRoutine(float duration, float mag)
        {

            float elapse = 0.00f;
            elapse += Time.deltaTime;
            while (elapse < duration)
            {
                float x = Random.Range(-1f, 1f) * mag;
                float y = Random.Range(-1f, 1f) * mag;

                transform.localPosition = new Vector3(x, y + 0.75f, 0);

                elapse += Time.deltaTime;

                yield return null;
            }
            transform.localPosition = ogPosition;
        }

        public void vibrate()
        {
            StartCoroutine(shakeRoutine(1, 0.1f));
        }
    }
}