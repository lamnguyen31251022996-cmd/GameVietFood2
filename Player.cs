using System.Collections.Generic;

namespace QuanAnViet.Models
{
    // ============================================================
    //  NGƯỜI CHƠI + QUÁN ĂN
    //  Giữ toàn bộ trạng thái: tiền, ngày, cấp quán, món đã mở khóa.
    // ============================================================
    public class Player
    {
        public string Name { get; private set; }
        public int Money { get; private set; }
        public int Day { get; private set; }
        public int Level { get; private set; }            // cấp quán 1-4
        public int TongSao { get; private set; }          // tổng số sao đã nhận
        public int SoKhachDaPhucVu { get; private set; }

        // Danh sách TÊN các món đã mở khóa
        public List<string> MonDaMoKhoa { get; private set; }

        public Player(string name)
        {
            Name = name;
            Money = 500000;   // vốn ban đầu 500.000đ
            Day = 1;
            Level = 1;
            TongSao = 0;
            SoKhachDaPhucVu = 0;

            // Khởi đầu chỉ có 2 món dễ nhất
            MonDaMoKhoa = new List<string>();
            MonDaMoKhoa.Add("Bánh mì");
            MonDaMoKhoa.Add("Phở");
        }

        public void ThemTien(int soTien)
        {
            Money += soTien;
        }

        // Trả về true nếu trừ tiền thành công, false nếu không đủ tiền.
        // Kiểu bool giúp nơi gọi biết có nên tiếp tục hay không.
        public bool TruTien(int soTien)
        {
            if (Money < soTien) return false;
            Money -= soTien;
            return true;
        }

        public void SangNgayMoi()
        {
            Day++;
        }

        public void LenCap()
        {
            if (Level < 4) Level++;
        }

        public void GhiNhanSao(int sao)
        {
            TongSao += sao;
            SoKhachDaPhucVu++;
        }

        public double SaoTrungBinh()
        {
            if (SoKhachDaPhucVu == 0) return 0;
            return (double)TongSao / SoKhachDaPhucVu;
        }

        public string TenCapQuan()
        {
            if (Level == 1) return "Quán nhỏ";
            if (Level == 2) return "Quán ăn gia đình";
            if (Level == 3) return "Nhà hàng Việt Nam";
            return "Nhà hàng nổi tiếng";
        }

        // Quán càng lớn, mỗi ngày càng đông khách
        public int SoKhachMoiNgay()
        {
            return 2 + Level;
        }

        // Chi phí nâng cấp lên cấp tiếp theo
        public int ChiPhiNangCap()
        {
            if (Level == 1) return 1500000;
            if (Level == 2) return 4000000;
            if (Level == 3) return 10000000;
            return 0; // đã max cấp
        }
    }
}
