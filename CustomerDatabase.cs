using System.Collections.Generic;
using QuanAnViet.Models;

namespace QuanAnViet.Data
{
    // ============================================================
    //  KHO KHÁCH HÀNG — khách Việt Nam và khách nước ngoài.
    //  Mỗi khách khác nhau ở tên, món yêu thích, mức kiên nhẫn
    //  và mức tip — KHÔNG gán tính cách theo quốc tịch.
    // ============================================================
    public static class CustomerDatabase
    {
        public static List<Customer> TatCaKhach()
        {
            List<Customer> ds = new List<Customer>();

            // --- Khách Việt Nam ---
            ds.Add(new Customer("Lan", "Việt Nam", "Bánh mì", 4, 10,
                "Chào bạn! Cho mình một phần nhé, mình đang vội đi học."));
            ds.Add(new Customer("Minh", "Việt Nam", "Cơm tấm", 3, 15,
                "Chào quán! Cho tôi một phần ăn trưa nhanh gọn nhé."));
            ds.Add(new Customer("Bà Tư", "Việt Nam", "Bánh cuốn", 5, 8,
                "Chào con, cho bà một phần nhẹ nhàng nhé."));
            ds.Add(new Customer("Hùng", "Việt Nam", "Bún bò Huế", 3, 12,
                "Quán ơi, cho mình một tô đầy đặn nha!"));
            ds.Add(new Customer("Cô Hạnh", "Việt Nam", "Canh chua", 4, 14,
                "Gia đình cô ghé ăn, cho cô một phần nhé."));

            // --- Khách nước ngoài ---
            ds.Add(new Customer("Yuki", "Nhật Bản", "Phở", 4, 20,
                "Xin chào! Tôi muốn thử món ăn Việt Nam."));
            ds.Add(new Customer("Min-jun", "Hàn Quốc", "Bún thịt nướng", 3, 18,
                "Xin chào! Bạn tôi giới thiệu quán này."));
            ds.Add(new Customer("James", "Mỹ", "Bánh mì", 3, 25,
                "Hello! Tôi nghe nói món này rất nổi tiếng."));
            ds.Add(new Customer("Pierre", "Pháp", "Gỏi cuốn", 5, 22,
                "Xin chào! Tôi muốn ăn gì đó tươi mát."));
            ds.Add(new Customer("Anna", "Đức", "Bánh xèo", 4, 20,
                "Xin chào! Hôm nay tôi muốn thử món mới."));
            ds.Add(new Customer("Carlos", "Tây Ban Nha", "Cơm tấm", 4, 16,
                "Xin chào! Quán có món nào đặc trưng không?"));

            return ds;
        }
    }
}
