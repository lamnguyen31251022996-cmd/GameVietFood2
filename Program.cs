using System;
using System.Text;

namespace QuanAnViet
{
    // Lop khoi dong. Nhiem vu duy nhat: bat cong tac cho game chay.
    class Program
    {
        static void Main(string[] args)
        {
            // Bat UTF-8 de tieng Viet co dau hien thi dung tren terminal
            Console.OutputEncoding = Encoding.UTF8;

            Game game = new Game();
            game.Run();
        }
    }
}
