using System;

namespace QuanAnViet.UI
{
    // ============================================================
    //  LỚP HIỂN THỊ
    //  Toàn bộ lệnh in ra màn hình được gom về đây.
    //  Vì sao gom một chỗ? Vì khi chuyển sang Godot (Phase 11),
    //  bạn chỉ phải thay file này — logic game giữ nguyên 100%.
    //  "static" = không cần new, gọi thẳng Screen.TieuDe(...)
    // ============================================================
    public static class Screen
    {
        public static void TieuDe(string chu)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("   " + chu);
            Console.WriteLine("==================================================");
        }

        public static void Ngan()
        {
            Console.WriteLine("--------------------------------------------------");
        }

        public static void ChoNhanPhim()
        {
            Console.WriteLine();
            Console.WriteLine("  (Nhấn phím bất kỳ để tiếp tục...)");
            Console.ReadKey(true);
        }

        // Đọc một số nguyên, bắt buộc nằm trong khoảng [min, max].
        // Người chơi gõ bậy -> hỏi lại, KHÔNG làm game crash.
        public static int DocSo(int min, int max)
        {
            while (true)
            {
                string nhap = Console.ReadLine();
                int so;
                if (int.TryParse(nhap, out so) && so >= min && so <= max)
                {
                    return so;
                }
                Console.Write("  [!] Vui lòng nhập số từ " + min + " đến " + max + ": ");
            }
        }

        public static string Tien(int soTien)
        {
            return soTien.ToString("N0") + "đ";
        }

        // Vẽ thanh kiên nhẫn kiểu [###--]
        public static string ThanhKienNhan(int hienTai, int toiDa)
        {
            string thanh = "";
            for (int i = 0; i < toiDa; i++)
            {
                thanh += (i < hienTai) ? "#" : "-";
            }
            return "[" + thanh + "]";
        }

        public static string Sao(int soSao)
        {
            string s = "";
            for (int i = 0; i < 5; i++)
            {
                s += (i < soSao) ? "*" : ".";
            }
            return s;
        }
    }
}
