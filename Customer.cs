namespace QuanAnViet.Models
{
    // ============================================================
    //  KHÁCH HÀNG
    //  Mỗi khách là một object riêng, có kiên nhẫn riêng.
    // ============================================================
    public class Customer
    {
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string FavoriteFood { get; private set; }
        public int Patience { get; private set; }      // kiên nhẫn hiện tại
        public int MaxPatience { get; private set; }   // kiên nhẫn tối đa (để vẽ thanh)
        public int TipPercent { get; private set; }    // % tiền tip nếu hài lòng
        public string Greeting { get; private set; }   // câu chào riêng

        public Customer(string name, string country, string favoriteFood,
                        int patience, int tipPercent, string greeting)
        {
            Name = name;
            Country = country;
            FavoriteFood = favoriteFood;
            Patience = patience;
            MaxPatience = patience;
            TipPercent = tipPercent;
            Greeting = greeting;
        }

        // Tạo một BẢN SAO mới từ khách mẫu.
        // Vì sao cần? Vì nếu dùng thẳng khách trong danh sách gốc,
        // khách đó mất kiên nhẫn một lần là mất vĩnh viễn ở những ngày sau.
        public Customer TaoBanSao()
        {
            return new Customer(Name, Country, FavoriteFood, MaxPatience, TipPercent, Greeting);
        }

        public void GiamKienNhan(int soLuong)
        {
            Patience -= soLuong;
            if (Patience < 0) Patience = 0;
        }

        public bool DaBoDi()
        {
            return Patience <= 0;
        }
    }
}
