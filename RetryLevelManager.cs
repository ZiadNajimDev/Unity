using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hardest_game_project
{
    public class RetryLevelManager : MonoBehaviour
    {
        public Image img;

        // Start is called before the first frame update
        void Start()
        {
            if (GameManager.bgtexture)
            {
                Texture2D tex2d = GameManager.bgtexture;
                img.sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height), Vector2.zero);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}