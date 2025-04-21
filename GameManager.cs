using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hardest_game_project
{
    //Game manager class
    public class GameManager : MonoBehaviour
    {
        public static Texture2D bgtexture = null;
        public static int maxLevel = 31;//Number of levels

        //get/set current level
        public static int Current_level
        {
            get
            {
                return PlayerPrefs.GetInt("current_play_level", 1);
            }
            set
            {
                PlayerPrefs.SetInt("current_play_level", value);
            }
        }
    }
}