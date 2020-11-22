using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Player player;
    public GameObject UIPlayerStats;

    private void Update()
    {
        MovePlayerByMouse();
        ShowPlayerStats();
    }

    private void MovePlayerByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 playerPos = player.transform.position;
            Vector2 raycastPos = ray.origin;
            Vector2 direction = raycastPos - playerPos;

            if (Math.Abs(direction.x) <= 2 && Math.Abs(direction.y) <= 2)
            {
                List<float> distToDir = new List<float>();
                foreach (Vector2 playerDir in player.directions)
                {
                    float magnitudeToDirection = Vector2.SqrMagnitude(playerDir - direction);
                    distToDir.Add(magnitudeToDirection);
                }

                float minVal = distToDir.Min();
                int index = distToDir.IndexOf(minVal);
                Vector2 endPoint = playerPos + player.directions[index];

                StartCoroutine(player.SmoothMove(endPoint));
            }
        }
    }

    private void ShowPlayerStats()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UIPlayerStats.SetActive(!UIPlayerStats.activeInHierarchy);
        }
    }
}
