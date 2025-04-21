using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hardest_game_project
{

    public class CameraMove : MonoBehaviour
    {
        //
        public float damping = 1.5f;
        public Transform _target = null;
        public Vector2 offset = new Vector2(2f, 1f);

        private bool faceLeft;
        private int lastX;
        private float dynamicSpeed;

        void Start()
        {
        }

        public void FindPlayer()
        {
            lastX = Mathf.RoundToInt(_target.position.x);
        }

        void LateUpdate()
        {
            if (_target)
            {
                int currentX = Mathf.RoundToInt(_target.position.x);
                if (currentX > lastX) faceLeft = false; else if (currentX < lastX) faceLeft = true;
                lastX = Mathf.RoundToInt(_target.position.x);

                Vector3 target;
                if (faceLeft)
                {
                    target = new Vector3(_target.position.x - offset.x, _target.position.y + offset.y + dynamicSpeed, transform.position.z);
                }
                else
                {
                    target = new Vector3(_target.position.x + offset.x, _target.position.y + offset.y + dynamicSpeed, transform.position.z);
                }
                Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
                transform.position = currentPosition;
            }
            else
            {
                GameObject tPlayer = GameObject.FindGameObjectWithTag("Player");
                if (tPlayer)
                {
                    _target = tPlayer.transform;
                    offset = new Vector2(Mathf.Abs(offset.x), offset.y);
                    FindPlayer();
                }
            }
        }
    }
}