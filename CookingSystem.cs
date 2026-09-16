using System;
using System.Collections.Generic;
using QuanAnViet.Models;
using QuanAnViet.UI;

namespace QuanAnViet.Systems
{
    // ============================================================
    //  HỆ THỐNG NẤU ĂN — phần gameplay chính.
    //  Luật chơi: các bước nấu bị xáo trộn, người chơi phải chọn
    //  ĐÚNG THỨ TỰ. Chọn sai -> khách mất kiên nhẫn.
    //  Trả về: điểm chất lượng món ăn (0-100).
    // ============================================================
    public static class CookingSystem
    {
        private static Random rnd = new Random();

        public static int Nau(Food mon, Customer khach)
        {
            Screen.TieuDe("BẾP  |  Đang nấu: " + mon.Icon + " " + mon.Name);
            Console.WriteLine("  Khách: " + khach.Name + " (" + khach.Country + ")");
            Console.WriteLine("  Nguyên liệu: " + string.Join(", ", mon.Ingredients));
            Console.WriteLine("  Độ khó: " + mon.TenDoKho());
            Screen.Ngan();

            // Tạo bản sao danh sách bước rồi XÁO TRỘN để hiển thị
            List<string> conLai = new List<string>(mon.Steps);
            XaoTron(conLai);

            int buocHienTai = 0;   // đang cần làm bước thứ mấy (theo thứ tự đúng)
            int soLanSai = 0;

            while (buocHienTai < mon.Steps.Count && !khach.DaBoDi())
            {
                Console.WriteLine();
                Console.WriteLine("  Bước " + (buocHienTai + 1) + "/" + mon.Steps.Count
                    + "   Kiên nhẫn của khách: " + Screen.ThanhKienNhan(khach.Patience, khach.MaxPatience));
                Console.WriteLine("  Chọn thao tác tiếp theo:");

                for (int i = 0; i < conLai.Count; i++)
                {
                    Console.WriteLine("    " + (i + 1) + ". " + conLai[i]);
                }

                Console.Write("  Lựa chọn: ");
                int chon = Screen.DocSo(1, conLai.Count);
                string thaoTac = conLai[chon - 1];

                if (thaoTac == mon.Steps[buocHienTai])
                {
                    Console.WriteLine("  [OK] " + thaoTac);
                    conLai.RemoveAt(chon - 1);   // bước đã làm thì bỏ khỏi danh sách
                    buocHienTai++;
                }
                else
                {
                    soLanSai++;
                    khach.GiamKienNhan(1);
                    Console.WriteLine("  [X] Sai thứ tự! Khách bắt đầu sốt ruột.");
                }
            }

            // Khách hết kiên nhẫn giữa chừng -> món coi như hỏng
            if (khach.DaBoDi())
            {
                return 0;
            }

            // Tính điểm chất lượng
            int chatLuong = 100;
            chatLuong -= soLanSai * 15;              // mỗi lần sai trừ 15 điểm
            chatLuong -= (mon.Difficulty - 1) * 5;   // món khó thì khó đạt điểm tuyệt đối
            if (chatLuong < 0) chatLuong = 0;
            if (chatLuong > 100) chatLuong = 100;

            Console.WriteLine();
            Console.WriteLine("  >>> Hoàn thành " + mon.Name + "! Chất lượng: " + chatLuong + "/100");
            if (soLanSai == 0)
            {
                Console.WriteLine("  >>> Nấu chuẩn không sai bước nào!");
            }
            Screen.ChoNhanPhim();

            return chatLuong;
        }

        // Thuật toán xáo trộn Fisher-Yates: đổi chỗ ngẫu nhiên từ cuối về đầu
        private static void XaoTron(List<string> ds)
        {
            for (int i = ds.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                string tam = ds[i];
                ds[i] = ds[j];
                ds[j] = tam;
            }
        }
    }
}
