using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace hardest_game_project
{
    //Configure level selection screen.
    public class AutoLayouter : MonoBehaviour
    {
        public GameObject trophy_item;//Trophy button
        public GameObject rocked_item;//Lock icon

        // Start is called before the first frame update
        void Start()
        {
            GridLayoutGroup grid = this.GetComponent<GridLayoutGroup>();
            for (int i = 1; i <= GameManager.maxLevel; i++)
            {
                int level_enable_ = PlayerPrefs.GetInt("level_enable_" + i.ToString(), ((i == 1) ? 1 : 0));
                GameObject obj = null;
                if (level_enable_ == 0)
                {
                    obj = Instantiate(rocked_item);
                    obj.name = System.String.Empty;
                }
                else
                {
                    obj = Instantiate(trophy_item);
                    obj.name = i.ToString();
                }

                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = i.ToString() + "\n";
                obj.transform.SetParent(grid.transform, false);
                obj.transform.localPosition = Vector3.zero;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

