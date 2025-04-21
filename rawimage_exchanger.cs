using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hardest_game_project
{
    public class rawimage_exchanger : MonoBehaviour
    {
        public float span = 0.2f;
        public Sprite[] sprites;
        private int counter;

        // Start is called before the first frame update
        void Start()
        {
            counter = 0;
            if (sprites.Length > 0)
            {
                this.GetComponent<RawImage>().texture = sprites[counter].texture;
                StartCoroutine("Exchanger");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator Exchanger()
        {
            while (true)
            {
                yield return new WaitForSeconds(span);
                counter++;
                if (counter > sprites.Length - 1)
                {
                    counter = 0;
                }
                this.GetComponent<RawImage>().texture = sprites[counter].texture;


            }
        }
    }
}