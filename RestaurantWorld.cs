using UnityEngine;

namespace QuanAnVietUnity
{
    public class RestaurantWorld : MonoBehaviour
    {
        void Start()
        {
            var bg = new GameObject("Restaurant_Background");
            var sr = bg.AddComponent<SpriteRenderer>();
            var tex = Resources.Load<Texture2D>("Art/RestaurantBackground");
            if (tex != null)
            {
                sr.sprite = Sprite.Create(
                    tex, new Rect(0, 0, tex.width, tex.height),
                    new Vector2(.5f, .5f), 60);
            }
            bg.transform.position = new Vector3(0, 0, 5);
            bg.transform.localScale = new Vector3(1.35f, .78f, 1);

            var kitchen = new GameObject("Kitchen");
            kitchen.transform.position = new Vector3(5.5f, 2.4f, 0);
            kitchen.AddComponent<KitchenPoint>();
        }
    }
}
