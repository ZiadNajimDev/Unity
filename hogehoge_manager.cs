using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace hardest_game_project
{
    public class hogehoge_manager : MonoBehaviour
    {
        public GameObject title;
        public GameObject selector;
        public GameObject goNext;
        public GameObject retryLevel;

        // Start is called before the first frame update
        void Start()
        {
            Instantiate(title, Vector3.zero, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {

        }
        void Awake()
        {
        }

        void delete()
        {
            GameObject[] c = GameObject.FindGameObjectsWithTag("title");
            foreach (GameObject cc in c)
            {
                GameObject.Destroy(cc);
            }
        }

        public void clicked_playbutton()
        {
            delete();

            Instantiate(selector, Vector3.zero, Quaternion.identity);
        }
        public void clicked_returntitlebutton()
        {
            delete();

            Instantiate(title, Vector3.zero, Quaternion.identity);
        }
        public void clicked_levelbutton(GameObject button)
        {
            delete();

            int id = int.Parse(button.name);

            GameManager.Current_level = int.Parse(button.name);

            string level_name = string.Format("Levels/Level{0}", GameManager.Current_level);
            var obj = Resources.Load<GameObject>(level_name);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        public void clicked_Load_retry()
        {
            delete();

            Instantiate(retryLevel, Vector3.zero, Quaternion.identity);
        }
        public void clicked_retry_level()
        {
            delete();

            int current = GameManager.Current_level;

            int id = current;

            string level_name = string.Format("Levels/Level{0}", GameManager.Current_level);
            var obj = Resources.Load<GameObject>(level_name);
            Instantiate(obj, Vector3.zero, Quaternion.identity);
        }
        public void Cleared(int level)
        {
            delete();

            Instantiate(goNext, Vector3.zero, Quaternion.identity);
        }
        public void nextLevel()
        {
            delete();

            clicked_retry_level();
        }
    }
}