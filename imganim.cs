using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hardest_game_project
{
    public class imganim : MonoBehaviour
    {
        public float scrollSpeed = 1f;
        public Renderer rend;
        void Start()
        {

        }

        void FixedUpdate()
        {
            float offset = Time.time * scrollSpeed;

            this.GetComponent<RawImage>().uvRect = new Rect(0, offset, 4.5f, 3.0f);
        }
    }
}