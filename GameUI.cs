using UnityEngine;

namespace QuanAnVietUnity
{
    public class GameUI : MonoBehaviour
    {
        GUIStyle title, box, small, button;

        void Start()
        {
            title = new GUIStyle(GUI.skin.label) { fontSize = 23, fontStyle = FontStyle.Bold };
            box = new GUIStyle(GUI.skin.box) { fontSize = 17, wordWrap = true, alignment = TextAnchor.MiddleCenter };
            small = new GUIStyle(GUI.skin.label) { fontSize = 16, wordWrap = true };
            button = new GUIStyle(GUI.skin.button) { fontSize = 16 };
        }

        void OnGUI()
        {
            var gm = GameManager.Instance;
            if (gm == null) return;

            GUI.Label(new Rect(18, 10, 900, 36), gm.GetStatus(), title);
            GUI.Box(new Rect(18, 52, 900, 78), gm.Message, box);

            GUI.Label(new Rect(18, 140, 1000, 32),
                "WASD / Mũi tên: di chuyển   •   E: nấu / phục vụ", small);

            if (gm.CurrentOrder != null)
            {
                string steps = "";
                for (int i = 0; i < gm.CurrentOrder.steps.Length; i++)
                    steps += (i + 1) + ". " + gm.CurrentOrder.steps[i] + "\n";

                GUI.Box(new Rect(Screen.width - 335, 15, 315, 245),
                    $"ĐƠN HÀNG\n\n🍜 {gm.CurrentOrder.name}\n" +
                    $"Giá bán: {gm.CurrentOrder.price:n0}đ\n" +
                    $"Nguyên liệu: {gm.CurrentOrder.ingredientCost:n0}đ\n\n" +
                    "CÁC BƯỚC:\n" + steps, box);
            }

            if (gm.IsCooking)
            {
                GUI.Box(new Rect(Screen.width / 2 - 190, Screen.height - 92, 380, 60),
                    $"🔥 Đang nấu... {(gm.CookingProgress * 100):0}%\n" +
                    $"Bước: {gm.CookingStep + 1}/{gm.CurrentOrder.steps.Length}",
                    box);
            }
            else if (gm.FoodReady)
            {
                GUI.Box(new Rect(Screen.width / 2 - 190, Screen.height - 92, 380, 60),
                    "🍽️ MÓN ĐÃ XONG — đi đến khách và nhấn E", box);
            }
        }
    }
}
