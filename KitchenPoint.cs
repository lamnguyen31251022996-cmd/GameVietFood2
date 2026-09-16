using UnityEngine;

namespace QuanAnVietUnity
{
    public class KitchenPoint : MonoBehaviour
    {
        public static KitchenPoint Instance { get; private set; }
        void Awake() { Instance = this; }
    }
}
