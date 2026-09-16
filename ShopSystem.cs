using System;
using System.Collections.Generic;
using QuanAnViet.Data;
using QuanAnViet.Models;
using QuanAnViet.UI;

namespace QuanAnViet.Systems
{
    // ============================================================
    //  CỬA HÀNG & NÂNG CẤP
    //  Người chơi dùng tiền để mở khóa món mới hoặc nâng cấp quán.
    // ============================================================
    public static class ShopSystem
    {
        public static void MoCuaHang(Player player)
        {
            bool dangMo = true;
            while (dangMo)
            {
                Screen.TieuDe("CỬA HÀNG  |  Tiền: " + Screen.Tien(player.Money));
                Console.WriteLine("  Quán hiện tại: " + player.TenCapQuan() + " (Cấp " + player.Level + ")");
                Screen.Ngan();
                Console.WriteLine("  1. Mở khóa món ăn mới");
                Console.WriteLine("  2. Nâng cấp quán");
                Console.WriteLine("  3. Quay lại");
                Console.Write("  Lựa chọn: ");

                int chon = Screen.DocSo(1, 3);
                if (chon == 1) MoKhoaMon(player);
                else if (chon == 2) NangCapQuan(player);
                else dangMo = false;
            }
        }

        private static void MoKhoaMon(Player player)
        {
            Screen.TieuDe("MỞ KHÓA MÓN ĂN  |  Tiền: " + Screen.Tien(player.Money));

            List<Food> tatCa = FoodDatabase.TatCaMon();
            List<Food> coTheMua = new List<Food>();

            foreach (Food mon in tatCa)
            {
                // Chỉ hiện món CHƯA mở khóa
                if (!player.MonDaMoKhoa.Contains(mon.Name))
                {
                    coTheMua.Add(mon);
                }
            }

            if (coTheMua.Count == 0)
            {
                Console.WriteLine("  Bạn đã mở khóa toàn bộ món ăn!");
                Screen.ChoNhanPhim();
                return;
            }

            for (int i = 0; i < coTheMua.Count; i++)
            {
                Food mon = coTheMua[i];
                string dieuKien = "";
                if (player.Level < mon.UnlockLevel)
                {
                    dieuKien = "  (cần quán cấp " + mon.UnlockLevel + ")";
                }
                Console.WriteLine("  " + (i + 1) + ". " + mon.Icon + " " + mon.Name
                    + " - " + Screen.Tien(mon.UnlockPrice)
                    + " | bán " + Screen.Tien(mon.Price)
                    + " | " + mon.TenDoKho() + dieuKien);
            }
            Console.WriteLine("  0. Quay lại");
            Console.Write("  Chọn món muốn mở khóa: ");

            int chon = Screen.DocSo(0, coTheMua.Count);
            if (chon == 0) return;

            Food monChon = coTheMua[chon - 1];

            if (player.Level < monChon.UnlockLevel)
            {
                Console.WriteLine("  [X] Quán chưa đủ cấp để nấu món này.");
            }
            else if (!player.TruTien(monChon.UnlockPrice))
            {
                Console.WriteLine("  [X] Không đủ tiền.");
            }
            else
            {
                player.MonDaMoKhoa.Add(monChon.Name);
                Console.WriteLine("  [OK] Đã thêm " + monChon.Name + " vào thực đơn!");
            }
            Screen.ChoNhanPhim();
        }

        private static void NangCapQuan(Player player)
        {
            Screen.TieuDe("NÂNG CẤP QUÁN");

            if (player.Level >= 4)
            {
                Console.WriteLine("  Quán của bạn đã đạt cấp cao nhất: " + player.TenCapQuan());
                Screen.ChoNhanPhim();
                return;
            }

            int chiPhi = player.ChiPhiNangCap();
            Console.WriteLine("  Hiện tại : " + player.TenCapQuan() + " (Cấp " + player.Level + ")");
            Console.WriteLine("  Chi phí  : " + Screen.Tien(chiPhi));
            Console.WriteLine("  Tiền có  : " + Screen.Tien(player.Money));
            Console.WriteLine("  Lợi ích  : đông khách hơn, mở được món khó hơn");
            Screen.Ngan();
            Console.WriteLine("  1. Nâng cấp ngay");
            Console.WriteLine("  2. Để sau");
            Console.Write("  Lựa chọn: ");

            int chon = Screen.DocSo(1, 2);
            if (chon == 1)
            {
                if (player.TruTien(chiPhi))
                {
                    player.LenCap();
                    Console.WriteLine("  [OK] Chúc mừng! Quán đã thành: " + player.TenCapQuan());
                }
                else
                {
                    Console.WriteLine("  [X] Không đủ tiền.");
                }
                Screen.ChoNhanPhim();
            }
        }
    }
}
