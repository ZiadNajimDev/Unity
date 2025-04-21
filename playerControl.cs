using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace hardest_game_project
{
    public class playerControl : MonoBehaviour
    {
        private float? lastMousePoint_x = null;
        private float? lastMousePoint_y = null;
        private Vector2? lastMousePoint = null;
        private int remaining_number_of_coins = 0;
        private int total_number_of_coins = 0;
        panel_top panel_top = null;
        [SerializeField] private float maxSpeed = 1.6f;
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject explosion_coin;
        bool enterGoal = false;

        void Start()
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");
            total_number_of_coins = coins.Length;
            remaining_number_of_coins = total_number_of_coins;

            var gpanel_top = GameObject.FindGameObjectWithTag("Panel_top");
            if (gpanel_top)
            {
                panel_top = gpanel_top.GetComponent<panel_top>();
                panel_top.SetText_level("Level" + GameManager.Current_level.ToString());
            }


            update_coin(total_number_of_coins - remaining_number_of_coins);

            //StartCoroutine(RecordFrame());
        }

        void update_coin(int get_coins)
        {
            if (panel_top)
            {
                panel_top.SetText_coin(get_coins, total_number_of_coins);
            }
        }
        private void Update()
        {
            if (remaining_number_of_coins == 0)
            {
                var goal = GameObject.FindGameObjectWithTag("goal");
                goal.GetComponent<Tilemap>().color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 0.5f));
            }

            Rigidbody2D rg = this.GetComponent<Rigidbody2D>();
            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastMousePoint_x = Input.mousePosition.x;
                    lastMousePoint_y = Input.mousePosition.y;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    lastMousePoint_x = null;
                    lastMousePoint_y = null;
                    rg.linearVelocity = Vector2.zero;
                }
                else if (lastMousePoint_x != null)
                {
                    float difference_x = Input.mousePosition.x - lastMousePoint_x.Value;
                    float difference_y = Input.mousePosition.y - lastMousePoint_y.Value;
                    float ddd = 4f;

                    rg.AddForce(new Vector2((difference_x * ddd), (difference_y * ddd)), ForceMode2D.Impulse);

                    lastMousePoint_x = Input.mousePosition.x;
                    lastMousePoint_y = Input.mousePosition.y;
                }
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastMousePoint_x = Input.mousePosition.x;
                    lastMousePoint_y = Input.mousePosition.y;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lastMousePoint_x = null;
                    lastMousePoint_y = null;
                    rg.linearVelocity = Vector2.zero;
                }
                else if (lastMousePoint_x != null)
                {
                    float difference_x = Input.mousePosition.x - lastMousePoint_x.Value;
                    float difference_y = Input.mousePosition.y - lastMousePoint_y.Value;

                    rg.AddForce(new Vector2((difference_x / 0.2f), (difference_y / 0.2f)), ForceMode2D.Force);
                    lastMousePoint_x = Input.mousePosition.x;
                    lastMousePoint_y = Input.mousePosition.y;
                }
            }
            if (rg.linearVelocity.magnitude > maxSpeed)
            {
                rg.linearVelocity = rg.linearVelocity.normalized * maxSpeed;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var tag = collision.gameObject.tag;
            if (tag == "coin")
            {
                Instantiate(explosion_coin, collision.gameObject.transform.position, Quaternion.identity);
                GameObject.Destroy(collision.gameObject);
                remaining_number_of_coins--;

                //UPDATE COIN COUNT
                update_coin(total_number_of_coins - remaining_number_of_coins);
            }
            else if (tag == "goal" && remaining_number_of_coins == 0)
            {
                this.enterGoal = true;

                //GO NEXT
                //RETURN MENU
                Invoke("DelayMethod_GoNext", 0.2f);
            }
            else if (tag == "enemy" && !this.enterGoal)
            {
                Instantiate(explosion, this.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);

                //RETRY
                //RETURN MENU
                Invoke("DelayMethod_RetryLevel", 1.2f);

            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            var tag = collision.gameObject.tag;
            if (tag == "goal")
            {
                this.enterGoal = false;
            }
        }
        void DelayMethod_GoNext()
        {
            this.gameObject.SetActive(false);
            int next_level = GameManager.Current_level + 1;
            if (next_level <= GameManager.maxLevel)
            {
                PlayerPrefs.SetInt("level_enable_" + next_level.ToString(), 1);
                GameManager.Current_level = next_level;
            }
            else
            {
                next_level = 1;

                PlayerPrefs.SetInt("level_enable_" + next_level.ToString(), 1);
                GameManager.Current_level = next_level;
            }

            GameObject obj = GameObject.Find("DemoManager");
            obj.GetComponent<hogehoge_manager>().Cleared(next_level);
        }

        void DelayMethod_RetryLevel()
        {
            GameObject obj = GameObject.Find("DemoManager");
            obj.GetComponent<hogehoge_manager>().clicked_Load_retry();
        }

        // IEnumerator RecordFrame()
        // {
        //     yield return new WaitForSeconds(0.4f);

        //     Texture2D texture = UnityEngine.ScreenCapture.CaptureScreenshotAsTexture();
        //     if (GameManager.bgtexture)
        //     {
        //         Object.Destroy(GameManager.bgtexture);
        //     }
        //     GameManager.bgtexture = texture;
        // }
    }
}