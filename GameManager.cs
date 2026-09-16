using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuanAnVietUnity
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public PlayerData Player { get; private set; } = new PlayerData();
        public CustomerController CurrentCustomer { get; private set; }
        public FoodData CurrentOrder { get; private set; }
        public bool FoodReady { get; private set; }
        public bool IsCooking { get; private set; }
        public float CookingProgress { get; private set; }
        public int CookingStep { get; private set; }
        public string Message { get; private set; } = "Chào mừng đến Quán Ăn Việt!";

        private readonly List<CustomerData> customerPool = new List<CustomerData>();
        private int customersToday, servedToday, lostToday;
        private int revenueToday, costToday;
        private float customerPatienceTimer;

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            customerPool.AddRange(CustomerDatabase.All());
        }

        public void StartDay()
        {
            servedToday = lostToday = revenueToday = costToday = 0;
            customersToday = Mathf.Clamp(2 + Player.level, 3, 8);
            Message = $"Ngày {Player.day}: quán mở cửa! Hôm nay có {customersToday} khách.";
            SpawnNextCustomer();
        }

        void Update()
        {
            if (CurrentCustomer != null && !FoodReady && !IsCooking)
            {
                customerPatienceTimer -= Time.deltaTime;
                CurrentCustomer.SetPatience(Mathf.Clamp01(customerPatienceTimer / Mathf.Max(1f, CurrentCustomer.Data.patience * 10f)));
                if (customerPatienceTimer <= 0f)
                {
                    lostToday++;
                    Message = $"{CurrentCustomer.Data.name} đã chờ quá lâu và rời quán.";
                    CurrentCustomer.Leave();
                    StartCoroutine(NextCustomerAfterDelay());
                }
            }
        }

        void SpawnNextCustomer()
        {
            if (servedToday + lostToday >= customersToday)
            {
                Message = $"Hết ngày! Phục vụ {servedToday}/{customersToday} khách • Doanh thu {revenueToday:n0}đ • Chi phí {costToday:n0}đ.";
                Player.day++;
                return;
            }

            FoodReady = false;
            CurrentOrder = null;
            CookingProgress = 0;
            CookingStep = 0;

            var cd = customerPool[Random.Range(0, customerPool.Count)];
            CurrentCustomer = CustomerSpawner.Instance.Spawn(cd);

            var unlocked = new List<FoodData>();
            foreach (var f in FoodDatabase.All())
                if (Player.unlockedFoods.Contains(f.name)) unlocked.Add(f);

            var favorite = FoodDatabase.Find(cd.favoriteFood);
            CurrentOrder = (favorite != null && Player.unlockedFoods.Contains(favorite.name) && Random.value < .7f)
                ? favorite : unlocked[Random.Range(0, unlocked.Count)];

            CurrentCustomer.SetOrder(CurrentOrder);
            customerPatienceTimer = CurrentCustomer.Data.patience * 10f;
            Message = $"{cd.name} ({cd.country}) gọi {CurrentOrder.name}. {cd.greeting}";
        }

        public void TryStartCooking()
        {
            if (CurrentOrder == null || IsCooking || FoodReady) return;

            float kitchenDistance = Vector2.Distance(
                PlayerController.Instance.transform.position,
                KitchenPoint.Instance.transform.position);

            if (kitchenDistance > 1.7f)
            {
                Message = "Đến gần khu bếp rồi nhấn E để bắt đầu nấu.";
                return;
            }

            if (Player.money < CurrentOrder.ingredientCost)
            {
                Message = "Không đủ tiền mua nguyên liệu!";
                return;
            }

            Player.money -= CurrentOrder.ingredientCost;
            costToday += CurrentOrder.ingredientCost;
            StartCoroutine(CookRoutine());
        }

        IEnumerator CookRoutine()
        {
            IsCooking = true;
            CookingProgress = 0f;
            CookingStep = 0;

            float duration = 1.2f + CurrentOrder.difficulty * .65f;
            while (CookingProgress < 1f)
            {
                CookingProgress += Time.deltaTime / duration;
                CookingStep = Mathf.Clamp(
                    Mathf.FloorToInt(CookingProgress * CurrentOrder.steps.Length),
                    0, CurrentOrder.steps.Length - 1);
                Message = $"Đang nấu {CurrentOrder.name}: {CurrentOrder.steps[CookingStep]}";
                yield return null;
            }

            CookingProgress = 1f;
            CookingStep = CurrentOrder.steps.Length - 1;
            IsCooking = false;
            FoodReady = true;
            Message = $"✨ {CurrentOrder.name} đã hoàn thành! Mang món đến khách và nhấn E.";
        }

        public void TryServe()
        {
            if (CurrentCustomer == null || !FoodReady) return;

            float distance = Vector2.Distance(
                CurrentCustomer.transform.position,
                PlayerController.Instance.transform.position);

            if (distance > 1.7f)
            {
                Message = "Đến gần khách rồi nhấn E để phục vụ.";
                return;
            }

            int quality = Random.Range(78, 101) - (CurrentOrder.difficulty - 1) * 4;
            int stars = quality >= 90 ? 5 : quality >= 75 ? 4 : quality >= 60 ? 3 : 2;
            int tip = stars >= 4 ? CurrentOrder.price * CurrentCustomer.Data.tipPercent / 100 : 0;

            if (CurrentOrder.name == CurrentCustomer.Data.favoriteFood && stars >= 4)
                tip += CurrentOrder.price / 10;

            int earned = CurrentOrder.price + tip;
            Player.money += earned;
            Player.totalStars += stars;
            Player.served++;
            servedToday++;
            revenueToday += earned;

            Message = $"{CurrentCustomer.Data.name}: {stars}/5 ⭐  — {CurrentOrder.name} rất ngon!  +{earned:n0}đ";
            CurrentCustomer.Leave();
            StartCoroutine(NextCustomerAfterDelay());
        }

        IEnumerator NextCustomerAfterDelay()
        {
            yield return new WaitForSeconds(1.4f);
            SpawnNextCustomer();
        }

        public string GetStatus()
        {
            return $"💰 {Player.money:n0}đ   |   📅 Ngày {Player.day}   |   ⭐ {Player.totalStars}   |   🏪 Cấp {Player.level}";
        }
    }
}
