using UnityEngine;

namespace QuanAnVietUnity
{
    public class CustomerController : MonoBehaviour
    {
        public CustomerData Data { get; private set; }
        public FoodData Order { get; private set; }

        Vector3 target;
        bool leaving;
        SpriteRenderer sr;
        Texture2D sheet;
        float patience = 1f;

        public void Init(CustomerData data)
        {
            Data = data;
            sr = GetComponent<SpriteRenderer>();
            sheet = Resources.Load<Texture2D>("Art/CustomerSheet");
            target = new Vector3(Random.Range(-5f, 5f), Random.Range(-1.3f, 2.8f), 0);
            UpdateSprite();
        }

        public void SetOrder(FoodData food) { Order = food; }

        public void SetPatience(float value)
        {
            patience = value;
            UpdateSprite();
        }

        public void Leave()
        {
            leaving = true;
            target = new Vector3(11, 0, 0);
        }

        void Update()
        {
            transform.position = Vector3.MoveTowards(
                transform.position, target, 2.0f * Time.deltaTime);

            if (leaving && transform.position.x > 10)
                Destroy(gameObject);
        }

        void UpdateSprite()
        {
            if (sheet == null || sr == null) return;

            // Different frames give visual variety for Vietnamese and international guests.
            int index = Mathf.Abs((Data.name + Data.country).GetHashCode()) % 8;
            sr.sprite = Sprite.Create(
                sheet, new Rect(index * 32, 0, 32, 32),
                new Vector2(.5f, .15f), 32);

            sr.flipX = patience < .35f;
        }
    }
}
