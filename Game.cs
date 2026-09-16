using System;
using System.Collections.Generic;
using QuanAnViet.Data;
using QuanAnViet.Models;
using QuanAnViet.Systems;
using QuanAnViet.UI;

namespace QuanAnViet
{
    // ============================================================
    //  LỚP GAME — trái tim của toàn bộ chương trình.
    //  Nó KHÔNG tự làm mọi việc, mà điều phối các lớp khác:
    //     Game -> CookingSystem -> Food / Customer
    //          -> ShopSystem    -> Player
    // ============================================================
    class Game
    {
        private Player player;
        private bool dangChay;
        private Random rnd;

        public Game()
        {
            dangChay = true;
            rnd = new Random();
            player = null;   // sẽ tạo sau khi hỏi tên người chơi
        }

        // ---------- VÒNG LẶP CHÍNH ----------
        public void Run()
        {
            NhapTenNguoiChoi();

            while (dangChay)
            {
                HienThiMenuChinh();
                int chon = Screen.DocSo(1, 5);

                if (chon == 1) BatDauNgayMoi();
                else if (chon == 2) ShopSystem.MoCuaHang(player);
                else if (chon == 3) XemThucDon();
                else if (chon == 4) HienThiHuongDan();
                else Thoat();
            }
        }

        private void NhapTenNguoiChoi()
        {
            Screen.TieuDe("QUÁN ĂN VIỆT NAM");
            Console.WriteLine();
            Console.Write("  Tên chủ quán của bạn là gì? ");
            string ten = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ten)) ten = "Chủ quán";

            player = new Player(ten);

            Console.WriteLine();
            Console.WriteLine("  Chào " + player.Name + "! Quán nhỏ của bạn vừa khai trương.");
            Console.WriteLine("  Vốn ban đầu: " + Screen.Tien(player.Money));
            Console.WriteLine("  Thực đơn khởi đầu: Bánh mì, Phở");
            Screen.ChoNhanPhim();
        }

        private void HienThiMenuChinh()
        {
            Screen.TieuDe("QUÁN ĂN VIỆT NAM  -  " + player.TenCapQuan());
            Console.WriteLine("  Chủ quán : " + player.Name);
            Console.WriteLine("  Ngày     : " + player.Day);
            Console.WriteLine("  Tiền     : " + Screen.Tien(player.Money));
            Console.WriteLine("  Đánh giá : " + player.SaoTrungBinh().ToString("0.0") + "/5"
                              + "  (" + player.SoKhachDaPhucVu + " khách đã phục vụ)");
            Screen.Ngan();
            Console.WriteLine("  1. Mở cửa bán hàng (bắt đầu ngày mới)");
            Console.WriteLine("  2. Cửa hàng & nâng cấp");
            Console.WriteLine("  3. Xem thực đơn");
            Console.WriteLine("  4. Hướng dẫn chơi");
            Console.WriteLine("  5. Thoát game");
            Console.Write("  Lựa chọn: ");
        }

        // ---------- MỘT NGÀY BÁN HÀNG ----------
        private void BatDauNgayMoi()
        {
            int soKhach = player.SoKhachMoiNgay();
            int doanhThu = 0;
            int chiPhi = 0;
            int saoTrongNgay = 0;
            int khachBoDi = 0;
            int daPhucVu = 0;

            List<Customer> mauKhach = CustomerDatabase.TatCaKhach();

            Screen.TieuDe("NGÀY " + player.Day + " - QUÁN MỞ CỬA");
            Console.WriteLine("  Hôm nay dự kiến có " + soKhach + " khách ghé quán.");
            Screen.ChoNhanPhim();

            for (int i = 1; i <= soKhach; i++)
            {
                // Lấy ngẫu nhiên một khách mẫu rồi tạo bản sao riêng cho lượt này
                Customer khach = mauKhach[rnd.Next(mauKhach.Count)].TaoBanSao();
                Food mon = ChonMonChoKhach(khach);

                Screen.TieuDe("KHÁCH " + i + "/" + soKhach + "  -  NGÀY " + player.Day);
                Console.WriteLine("  Tên          : " + khach.Name);
                Console.WriteLine("  Quốc gia     : " + khach.Country);
                Console.WriteLine("  Kiên nhẫn    : " + Screen.ThanhKienNhan(khach.Patience, khach.MaxPatience));
                Screen.Ngan();
                Console.WriteLine("  \"" + khach.Greeting + "\"");
                Console.WriteLine("  \"Cho tôi một phần " + mon.Name + " nhé!\"");
                Screen.Ngan();
                Console.WriteLine("  Tiền nguyên liệu cần bỏ ra: " + Screen.Tien(mon.IngredientCost));

                if (!player.TruTien(mon.IngredientCost))
                {
                    Console.WriteLine("  [X] Bạn không đủ tiền mua nguyên liệu. Đành xin lỗi khách.");
                    khachBoDi++;
                    Screen.ChoNhanPhim();
                    continue;   // bỏ qua khách này, sang khách tiếp theo
                }

                chiPhi += mon.IngredientCost;
                Screen.ChoNhanPhim();

                int chatLuong = CookingSystem.Nau(mon, khach);

                if (khach.DaBoDi())
                {
                    Screen.TieuDe("KHÁCH BỎ ĐI");
                    Console.WriteLine("  " + khach.Name + " đã hết kiên nhẫn và rời quán.");
                    Console.WriteLine("  Bạn mất " + Screen.Tien(mon.IngredientCost) + " tiền nguyên liệu.");
                    khachBoDi++;
                    player.GhiNhanSao(1);
                    saoTrongNgay += 1;
                    Screen.ChoNhanPhim();
                    continue;
                }

                // --- Tính sao và tiền ---
                int sao = TinhSao(chatLuong);
                int tienMon = mon.Price;
                int tip = 0;

                if (sao >= 4)
                {
                    tip = mon.Price * khach.TipPercent / 100;
                    if (mon.Name == khach.FavoriteFood)
                    {
                        tip += mon.Price / 10;   // thưởng thêm vì đúng món yêu thích
                    }
                }

                player.ThemTien(tienMon + tip);
                player.GhiNhanSao(sao);
                doanhThu += tienMon + tip;
                saoTrongNgay += sao;
                daPhucVu++;

                // --- Phản ứng của khách ---
                Screen.TieuDe("PHỤC VỤ  -  " + khach.Name);
                Console.WriteLine("  Chất lượng món : " + chatLuong + "/100");
                Console.WriteLine("  Đánh giá       : " + Screen.Sao(sao) + " (" + sao + "/5)");
                Console.WriteLine("  \"" + PhanUng(sao, mon, khach) + "\"");
                Screen.Ngan();
                Console.WriteLine("  Tiền món : " + Screen.Tien(tienMon));
                Console.WriteLine("  Tiền tip : " + Screen.Tien(tip));
                Console.WriteLine("  TỔNG NHẬN: " + Screen.Tien(tienMon + tip));
                Screen.ChoNhanPhim();
            }

            // ---------- TỔNG KẾT CUỐI NGÀY ----------
            Screen.TieuDe("TỔNG KẾT NGÀY " + player.Day);
            Console.WriteLine("  Khách phục vụ thành công : " + daPhucVu);
            Console.WriteLine("  Khách bỏ đi              : " + khachBoDi);
            Screen.Ngan();
            Console.WriteLine("  Doanh thu   : " + Screen.Tien(doanhThu));
            Console.WriteLine("  Chi phí     : " + Screen.Tien(chiPhi));
            Console.WriteLine("  LỢI NHUẬN   : " + Screen.Tien(doanhThu - chiPhi));
            Screen.Ngan();
            Console.WriteLine("  Tiền hiện có: " + Screen.Tien(player.Money));
            Console.WriteLine("  Đánh giá TB : " + player.SaoTrungBinh().ToString("0.0") + "/5");
            Screen.ChoNhanPhim();

            player.SangNgayMoi();
        }

        // Chọn món cho khách: ưu tiên món yêu thích nếu quán đã có
        private Food ChonMonChoKhach(Customer khach)
        {
            if (player.MonDaMoKhoa.Contains(khach.FavoriteFood) && rnd.Next(100) < 70)
            {
                return FoodDatabase.TimTheoTen(khach.FavoriteFood);
            }

            string tenMon = player.MonDaMoKhoa[rnd.Next(player.MonDaMoKhoa.Count)];
            return FoodDatabase.TimTheoTen(tenMon);
        }

        private int TinhSao(int chatLuong)
        {
            if (chatLuong >= 90) return 5;
            if (chatLuong >= 75) return 4;
            if (chatLuong >= 60) return 3;
            if (chatLuong >= 40) return 2;
            return 1;
        }

        private string PhanUng(int sao, Food mon, Customer khach)
        {
            if (sao == 5) return "Tuyệt vời! " + mon.Name + " ở đây ngon thật, tôi sẽ quay lại!";
            if (sao == 4) return "Ngon lắm, cảm ơn quán nhé!";
            if (sao == 3) return "Cũng được, nhưng tôi nghĩ còn có thể ngon hơn.";
            if (sao == 2) return "Món hơi lâu và chưa đúng vị lắm.";
            return "Tôi không hài lòng với phần ăn này.";
        }

        // ---------- CÁC MÀN HÌNH PHỤ ----------
        private void XemThucDon()
        {
            Screen.TieuDe("THỰC ĐƠN CỦA QUÁN");
            foreach (string ten in player.MonDaMoKhoa)
            {
                Food mon = FoodDatabase.TimTheoTen(ten);
                if (mon == null) continue;
                Console.WriteLine("  " + mon.Icon + " " + mon.Name
                    + "  |  Giá bán: " + Screen.Tien(mon.Price)
                    + "  |  Vốn: " + Screen.Tien(mon.IngredientCost)
                    + "  |  " + mon.TenDoKho());
                Console.WriteLine("      Nguyên liệu: " + string.Join(", ", mon.Ingredients));
            }
            Screen.ChoNhanPhim();
        }

        private void HienThiHuongDan()
        {
            Screen.TieuDe("HƯỚNG DẪN CHƠI");
            Console.WriteLine("  Bạn là chủ một quán ăn Việt Nam.");
            Console.WriteLine();
            Console.WriteLine("  Mỗi ngày:");
            Console.WriteLine("    1. Khách vào quán và gọi món");
            Console.WriteLine("    2. Bạn tự động trả tiền nguyên liệu");
            Console.WriteLine("    3. Ở màn hình BẾP, các bước nấu bị xáo trộn");
            Console.WriteLine("       -> hãy chọn ĐÚNG THỨ TỰ nấu");
            Console.WriteLine("    4. Chọn sai -> khách mất 1 điểm kiên nhẫn");
            Console.WriteLine("       Hết kiên nhẫn -> khách bỏ đi, mất trắng tiền vốn");
            Console.WriteLine("    5. Nấu tốt -> nhiều sao -> có tiền tip");
            Console.WriteLine();
            Console.WriteLine("  Dùng tiền lãi để mở khóa món mới và nâng cấp quán:");
            Console.WriteLine("    Quán nhỏ -> Quán ăn gia đình -> Nhà hàng -> Nhà hàng nổi tiếng");
            Screen.ChoNhanPhim();
        }

        private void Thoat()
        {
            Screen.TieuDe("TẠM BIỆT");
            Console.WriteLine("  Chủ quán  : " + player.Name);
            Console.WriteLine("  Trụ được  : " + (player.Day - 1) + " ngày");
            Console.WriteLine("  Tài sản   : " + Screen.Tien(player.Money));
            Console.WriteLine("  Đánh giá  : " + player.SaoTrungBinh().ToString("0.0") + "/5");
            Console.WriteLine();
            Console.WriteLine("  Cảm ơn bạn đã chơi. Hẹn gặp lại!");
            dangChay = false;
        }
    }
}
