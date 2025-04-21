using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

namespace hardest_game_project
{
    //Move the enemy
    public class enemy : MonoBehaviour
    {
        public enum MoveDirection
        {
            moveLeft = 1,
            moveRight = 2,
            moveUp = 3,
            moveDown = 4,
        }

        [SerializeField] MoveDirection startDirection = MoveDirection.moveUp;
        [SerializeField] float unitDuration = 0.5f;

        Tilemap tilemap;
        Vector3Int currPos;
        MoveDirection curr_dir;
        MoveDirection prev_dir;


        // Start is called before the first frame update
        void Start()
        {
            tilemap = this.transform.parent.gameObject.GetComponent<Tilemap>();
            if (tilemap)
            {
                tilemap.GetComponent<TilemapRenderer>().enabled = false;
                currPos = tilemap.WorldToCell(this.transform.position);
                this.curr_dir = startDirection;
                this.prev_dir = 0;
                Vector3 position = this.get_next_position();
                StartCoroutine(AnimateCoroutine(this.transform, unitDuration, position, null, OnFinishedCoroutine));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector3 get_next_position()
        {
            Vector3Int seek_pos = Vector3Int.zero;

            TileBase currTile = tilemap.GetTile(currPos);

            switch (curr_dir)
            {
                case MoveDirection.moveLeft:
                    seek_pos = currPos + Vector3Int.left;
                    break;
                case MoveDirection.moveRight:
                    seek_pos = currPos + Vector3Int.right;
                    break;
                case MoveDirection.moveUp:
                    seek_pos = currPos + Vector3Int.up;
                    break;
                case MoveDirection.moveDown:
                    seek_pos = currPos + Vector3Int.down;
                    break;
            }

            if (tilemap.HasTile(seek_pos))
            {
                //If we have the tiles, we can proceed.
                currPos = seek_pos;
            }
            else
            {
                //If there are no tiles, go in the other direction
                //Other than the current direction
                if (curr_dir == MoveDirection.moveLeft)
                {
                    //Candidates other than the direction of travel
                    Vector3Int up = currPos + Vector3Int.up;
                    Vector3Int dn = currPos + Vector3Int.down;
                    Vector3Int rt = currPos + Vector3Int.right;
                    if (tilemap.HasTile(up))
                    {
                        curr_dir = MoveDirection.moveUp;//up
                        currPos = currPos + Vector3Int.up;
                    }
                    else if (tilemap.HasTile(dn))
                    {
                        curr_dir = MoveDirection.moveDown;//down
                        currPos = currPos + Vector3Int.down;
                    }
                    else
                    {
                        curr_dir = MoveDirection.moveRight;//right
                        currPos = currPos + Vector3Int.right;
                    }
                }
                else if (curr_dir == MoveDirection.moveRight)
                {
                    //Candidates other than the direction of travel
                    Vector3Int up = currPos + Vector3Int.up;
                    Vector3Int dn = currPos + Vector3Int.down;
                    Vector3Int lt = currPos + Vector3Int.left;
                    if (tilemap.HasTile(up))
                    {
                        curr_dir = MoveDirection.moveUp;//up
                        currPos = currPos + Vector3Int.up;
                    }
                    else if (tilemap.HasTile(dn))
                    {
                        curr_dir = MoveDirection.moveDown;//down
                        currPos = currPos + Vector3Int.down;
                    }
                    else
                    {
                        curr_dir = MoveDirection.moveLeft;//left
                        currPos = currPos + Vector3Int.left;
                    }
                }
                else if (curr_dir == MoveDirection.moveUp)
                {
                    //Candidates other than the direction of travel
                    Vector3Int lt = currPos + Vector3Int.left;
                    Vector3Int rt = currPos + Vector3Int.right;
                    Vector3Int dn = currPos + Vector3Int.down;
                    if (tilemap.HasTile(lt))
                    {
                        curr_dir = MoveDirection.moveLeft;//left
                        currPos = currPos + Vector3Int.left;
                    }
                    else if (tilemap.HasTile(rt))
                    {
                        curr_dir = MoveDirection.moveRight;//right
                        currPos = currPos + Vector3Int.right;
                    }
                    else
                    {
                        curr_dir = MoveDirection.moveDown;//down
                        currPos = currPos + Vector3Int.down;
                    }
                }
                else if (curr_dir == MoveDirection.moveDown)
                {
                    //Candidates other than the direction of travel
                    Vector3Int lt = currPos + Vector3Int.left;
                    Vector3Int rt = currPos + Vector3Int.right;
                    Vector3Int dn = currPos + Vector3Int.up;
                    if (tilemap.HasTile(lt))
                    {
                        curr_dir = MoveDirection.moveLeft;//left
                        currPos = currPos + Vector3Int.left;
                    }
                    else if (tilemap.HasTile(rt))
                    {
                        curr_dir = MoveDirection.moveRight;//right
                        currPos = currPos + Vector3Int.right;
                    }
                    else
                    {
                        curr_dir = MoveDirection.moveUp;//up
                        currPos = currPos + Vector3Int.up;
                    }
                }
            }

            prev_dir = curr_dir;
            return tilemap.GetCellCenterWorld(currPos); //currPos;
        }

        // tranform Transform you want to move
        // time     How many seconds it takes
        // position Target position...If you enter null, the value does not change
        // rotation Target rotation... If you put null, the value does not change
        IEnumerator AnimateCoroutine(Transform transform, float time, Vector3? position, Quaternion? rotation, UnityAction<int> callback)
        {
            // Current position, rotation
            var currentPosition = transform.position;
            var currentRotation = transform.rotation;

            // Target position, rotation
            var targetPosition = position ?? currentPosition;
            var targetRotation = rotation ?? currentRotation;

            var sumTime = 0f;
            while (true)
            {
                // How many seconds have passed since the Coroutine start frame
                sumTime += Time.deltaTime;
                // Percentage of time that has passed relative to the specified time
                var ratio = sumTime / time;

                transform.SetPositionAndRotation(
                    Vector3.Lerp(currentPosition, targetPosition, ratio),
                    Quaternion.Lerp(currentRotation, targetRotation, ratio)
                );

                if (ratio > 1.0f)
                {
                    // Exit this Coroutine when you reach the target value
                    // Lerp is an argument that indicates the ratio is clamped between 0 and 1, so there is no problem even if it is larger than 1.
                    break;
                }

                yield return null;
            }

            OnFinishedCoroutine(1);
        }

        // At the end of coroutine
        public void OnFinishedCoroutine(int num)
        {
            Vector3 position = this.get_next_position();
            StartCoroutine(AnimateCoroutine(this.transform, unitDuration, position, null, OnFinishedCoroutine));
        }
    }
}