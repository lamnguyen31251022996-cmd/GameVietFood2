using UnityEngine;

namespace QuanAnVietUnity
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }
        public float speed = 3.5f;

        SpriteRenderer sr;
        Texture2D sheet;
        int direction = 0;
        float animTimer;
        int frame;

        void Awake()
        {
            Instance = this;
            sr = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            sheet = Resources.Load<Texture2D>("Art/PlayerSheet");
            transform.localScale = Vector3.one * 1.7f;
            UpdateSprite();
        }

        void Update()
        {
            Vector2 input = Vector2.zero;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) input.x += 1;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) input.x -= 1;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) input.y += 1;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) input.y -= 1;

            bool moving = input.sqrMagnitude > 0.01f;
            if (moving)
            {
                if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) direction = input.x > 0 ? 3 : 2;
                else direction = input.y > 0 ? 1 : 0;

                transform.position += (Vector3)(input.normalized * speed * Time.deltaTime);

                animTimer += Time.deltaTime;
                if (animTimer > .16f)
                {
                    animTimer = 0;
                    frame = 1 - frame;
                }
            }
            else frame = 0;

            UpdateSprite();

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -8.5f, 8.5f),
                Mathf.Clamp(transform.position.y, -3.8f, 3.8f),
                0);

            if (Input.GetKeyDown(KeyCode.E))
            {
                var gm = GameManager.Instance;
                gm.TryStartCooking();
                gm.TryServe();
            }
        }

        void UpdateSprite()
        {
            if (sheet == null || sr == null) return;
            int index = direction * 2 + frame;
            sr.sprite = Sprite.Create(
                sheet, new Rect(index * 32, 0, 32, 32),
                new Vector2(.5f, .15f), 32);
        }
    }
}
