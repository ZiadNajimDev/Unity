using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hardest_game_project
{
    public class panel_top : MonoBehaviour
    {
        public UnityEngine.UI.Text Text_coin;
        public UnityEngine.UI.Text Text_level;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetText_coin(int get_coins, int total_number_of_coins)
        {
            Text_coin.text = string.Format(" coin {0} / {1}", get_coins, total_number_of_coins);
        }

        public void SetText_level(string value)
        {
            Text_level.text = value + " ";
        }
    }
}