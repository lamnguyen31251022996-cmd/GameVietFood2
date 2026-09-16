using System.Collections;
using UnityEngine;

namespace QuanAnVietUnity
{
    public class Bootstrap : MonoBehaviour
    {
        void Awake()
        {
            if (GameManager.Instance == null)
            {
                var gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
            }

            var world = new GameObject("RestaurantWorld");
            world.AddComponent<RestaurantWorld>();

            var spawner = new GameObject("CustomerSpawner");
            spawner.AddComponent<CustomerSpawner>();

            var player = new GameObject("Player");
            player.transform.position = new Vector3(-2.5f, -2.3f, 0);
            player.AddComponent<SpriteRenderer>();
            player.AddComponent<PlayerController>();

            var cam = new GameObject("Main Camera");
            cam.tag = "MainCamera";
            cam.transform.position = new Vector3(0, 0, -10);
            var camera = cam.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 5.1f;

            var ui = new GameObject("GameUI");
            ui.AddComponent<GameUI>();
        }

        IEnumerator Start()
        {
            yield return null;
            GameManager.Instance.StartDay();
        }
    }
}
