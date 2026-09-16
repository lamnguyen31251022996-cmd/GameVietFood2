using System.Collections.Generic;
using QuanAnViet.Models;

namespace QuanAnViet.Data
{
    // ============================================================
    //  KHO MÓN ĂN — nơi khai báo sẵn 10 món truyền thống.
    //  Tách riêng khỏi logic để sau này thêm món mới chỉ cần
    //  thêm 1 dòng ở đây, không phải sửa chỗ nào khác.
    // ============================================================
    public static class FoodDatabase
    {
        public static List<Food> TatCaMon()
        {
            List<Food> ds = new List<Food>();

            ds.Add(new Food("Bánh mì", "[BM]", 25000, 8000, 1, 1, 0,
                new List<string> { "Bánh mì", "Pate", "Thịt nguội", "Dưa leo", "Rau thơm" },
                new List<string> { "Rạch ổ bánh mì", "Phết pate", "Cho thịt nguội", "Thêm dưa leo và rau", "Rưới nước sốt" }));

            ds.Add(new Food("Phở", "[PHO]", 50000, 18000, 2, 1, 0,
                new List<string> { "Bánh phở", "Thịt bò", "Nước dùng", "Hành lá", "Rau thơm" },
                new List<string> { "Ninh nước dùng", "Trụng bánh phở", "Xếp bánh vào tô", "Xếp thịt bò lên trên", "Chan nước dùng", "Rắc hành và rau thơm" }));

            ds.Add(new Food("Cơm tấm", "[CT]", 45000, 15000, 1, 1, 400000,
                new List<string> { "Cơm tấm", "Sườn nướng", "Đồ chua", "Mỡ hành", "Nước mắm" },
                new List<string> { "Ướp sườn", "Nướng sườn", "Xới cơm ra đĩa", "Đặt sườn lên cơm", "Thêm đồ chua và mỡ hành" }));

            ds.Add(new Food("Xôi", "[XOI]", 20000, 6000, 1, 1, 300000,
                new List<string> { "Gạo nếp", "Đậu xanh", "Hành phi", "Muối vừng" },
                new List<string> { "Ngâm gạo nếp", "Hấp xôi", "Trộn đậu xanh", "Rắc hành phi và muối vừng" }));

            ds.Add(new Food("Gỏi cuốn", "[GC]", 35000, 12000, 2, 2, 800000,
                new List<string> { "Bánh tráng", "Tôm luộc", "Thịt luộc", "Bún", "Rau sống", "Tương chấm" },
                new List<string> { "Nhúng mềm bánh tráng", "Xếp rau sống", "Xếp bún", "Đặt tôm và thịt", "Cuốn chặt tay", "Pha tương chấm" }));

            ds.Add(new Food("Bánh xèo", "[BX]", 55000, 20000, 2, 2, 1000000,
                new List<string> { "Bột bánh xèo", "Tôm", "Thịt ba chỉ", "Giá đỗ", "Rau sống" },
                new List<string> { "Pha bột với nghệ", "Làm nóng chảo", "Xào tôm thịt", "Đổ bột tráng mỏng", "Thêm giá đỗ", "Gập đôi bánh" }));

            ds.Add(new Food("Canh chua", "[CC]", 60000, 22000, 2, 2, 1000000,
                new List<string> { "Cá", "Me", "Cà chua", "Dứa", "Bạc hà", "Giá đỗ" },
                new List<string> { "Nấu nước me", "Cho cà chua và dứa", "Thả cá vào", "Nêm nếm gia vị", "Cho bạc hà và giá", "Rắc rau om" }));

            ds.Add(new Food("Bún thịt nướng", "[BTN]", 50000, 17000, 2, 3, 1500000,
                new List<string> { "Bún", "Thịt nướng", "Rau sống", "Đồ chua", "Nước mắm chua ngọt" },
                new List<string> { "Ướp thịt", "Nướng thịt", "Xếp rau sống vào tô", "Cho bún lên trên", "Xếp thịt nướng", "Chan nước mắm chua ngọt" }));

            ds.Add(new Food("Bún bò Huế", "[BBH]", 60000, 22000, 3, 3, 2000000,
                new List<string> { "Bún sợi to", "Thịt bò", "Giò heo", "Sả", "Mắm ruốc", "Ớt sa tế" },
                new List<string> { "Ninh xương với sả", "Pha mắm ruốc vào nước dùng", "Luộc giò heo", "Trụng bún", "Xếp thịt bò và giò", "Chan nước dùng", "Thêm ớt sa tế" }));

            ds.Add(new Food("Bánh cuốn", "[BC]", 40000, 14000, 3, 4, 2500000,
                new List<string> { "Bột gạo", "Thịt băm", "Mộc nhĩ", "Hành phi", "Nước chấm" },
                new List<string> { "Pha bột gạo loãng", "Xào nhân thịt mộc nhĩ", "Tráng bánh trên vải", "Cho nhân vào giữa", "Cuộn bánh lại", "Rắc hành phi và chan nước chấm" }));

            return ds;
        }

        // Tìm một món theo tên. Trả về null nếu không có.
        public static Food TimTheoTen(string ten)
        {
            List<Food> ds = TatCaMon();
            foreach (Food mon in ds)
            {
                if (mon.Name == ten) return mon;
            }
            return null;
        }
    }
}
