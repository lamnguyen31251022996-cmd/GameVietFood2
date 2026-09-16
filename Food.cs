using System.Collections.Generic;

namespace QuanAnViet.Models
{
    // ============================================================
    //  MÓN ĂN — đây là "bản thiết kế" của mọi món trong quán.
    //  Lưu ý: class này KHÔNG có Console.WriteLine nào cả.
    //  Nó chỉ chứa DỮ LIỆU. Việc hiển thị là của lớp khác.
    // ============================================================
    public class Food
    {
        // { get; private set; } = bên ngoài ĐỌC được, nhưng chỉ code
        // bên trong class này mới SỬA được -> tránh sửa nhầm giá tiền.
        public string Name { get; private set; }          // Tên món
        public string Icon { get; private set; }          // Biểu tượng
        public int Price { get; private set; }            // Giá bán cho khách
        public int IngredientCost { get; private set; }   // Tiền vốn nguyên liệu
        public int Difficulty { get; private set; }       // Độ khó 1-3
        public int UnlockLevel { get; private set; }      // Cần quán cấp mấy mới mua được
        public int UnlockPrice { get; private set; }      // Tiền để mở khóa món
        public List<string> Ingredients { get; private set; } // Danh sách nguyên liệu
        public List<string> Steps { get; private set; }        // Các bước nấu ĐÚNG THỨ TỰ

        // Hàm khởi tạo: bắt buộc phải cung cấp đủ thông tin khi tạo món mới
        public Food(string name, string icon, int price, int ingredientCost,
                    int difficulty, int unlockLevel, int unlockPrice,
                    List<string> ingredients, List<string> steps)
        {
            Name = name;
            Icon = icon;
            Price = price;
            IngredientCost = ingredientCost;
            Difficulty = difficulty;
            UnlockLevel = unlockLevel;
            UnlockPrice = unlockPrice;
            Ingredients = ingredients;
            Steps = steps;
        }

        public string TenDoKho()
        {
            if (Difficulty == 1) return "Dễ";
            if (Difficulty == 2) return "Trung bình";
            return "Khó";
        }
    }
}
