using UnityEngine;

namespace QuanAnVietUnity
{
    public class CustomerSpawner : MonoBehaviour
    {
        public static CustomerSpawner Instance { get; private set; }

        void Awake() { Instance = this; }

        public CustomerController Spawn(CustomerData data)
        {
            var go = new GameObject("Customer_" + data.name);
            var sr = go.AddComponent<SpriteRenderer>();
            var sheet = Resources.Load<Texture2D>("Art/CustomerSheet");

            if (sheet != null)
            {
                int index = Mathf.Abs((data.name + data.country).GetHashCode()) % 8;
                sr.sprite = Sprite.Create(
                    sheet, new Rect(index * 32, 0, 32, 32),
                    new Vector2(.5f, .15f), 32);
            }

            go.transform.position = new Vector3(-10, 0, 0);
            go.transform.localScale = Vector3.one * 1.7f;

            var cc = go.AddComponent<CustomerController>();
            cc.Init(data);
            return cc;
        }
    }
}
