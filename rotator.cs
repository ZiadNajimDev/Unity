using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hardest_game_project
{
    public class rotator : MonoBehaviour
    {
        [SerializeField]
        float rotatePerSec = 10;
        [SerializeField]
        float WaitForSeconds = 0;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(AnimatedRotate());
        }

        IEnumerator AnimatedRotate()
        {
            float seconds = 0;
            for (; ; )
            {

                seconds += Time.deltaTime;
                if (WaitForSeconds > 0f && seconds >= 10f)
                {
                    seconds = 0f;
                    yield return new WaitForSeconds(3);
                }

                float persec = 360.0f / rotatePerSec;
                float rotateValue = persec * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 16f) * Time.deltaTime, Space.World);
                yield return null;
            }
        }
    }
}