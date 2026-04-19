using UnityEngine;

namespace Config
{
    [CreateAssetMenu(
        fileName = "CameraFollowSettings",
        menuName = "Diledsys/Camera/Camera Follow Settings",
        order = 0)]
    public sealed class CameraFollowSettings : ScriptableObject
    {
        [Header("Target")]
        [Tooltip("Смещение точки слежения относительно цели (в локальных координатах цели).")]
        public Vector3 targetOffset = new Vector3(0f, 1.6f, 0f);

        [Header("Camera Position")]
        [Tooltip("Смещение камеры относительно точки слежения (в мировых координатах).")]
        public Vector3 cameraOffset = new Vector3(0f, 3.5f, -6f);

        [Tooltip("Сглаживание перемещения камеры. Меньше = резче, больше = плавнее.")]
        [Range(0.01f, 1.0f)]
        public float positionSmoothTime = 0.18f;

        [Header("Dead Zone (Viewport Rect)")]
        [Tooltip("Мёртвая зона в координатах viewport (0..1). Камера двигается только когда цель выходит за неё.")]
        public Rect deadZone = new Rect(0.35f, 0.25f, 0.30f, 0.50f);

        [Header("Limits (Optional)")]
        [Tooltip("Ограничить движение камеры по X/Z (например для уровней).")]
        public bool useBounds = false;

        public Vector2 minXZ = new Vector2(-9999f, -9999f);
        public Vector2 maxXZ = new Vector2(9999f, 9999f);

        [Header("Debug")]
        public bool drawGizmos = true;
	
	        [Header("2D Look-Ahead")]
        [Tooltip("Включить look-ahead (камера смотрит вперёд по направлению движения цели).")]
        public bool useLookAhead2D = true;

        [Tooltip("Насколько далеко камера смотрит вперёд (в мировых единицах).")]
        [Range(0f, 10f)]
        public float lookAheadDistance2D = 2.0f;

        [Tooltip("Сглаживание look-ahead. Меньше = резче, больше = плавнее.")]
        [Range(0.01f, 1.0f)]
        public float lookAheadSmoothTime2D = 0.15f;

        [Tooltip("Минимальная скорость цели, после которой включаем look-ahead (чтобы не дрожало на месте).")]
        [Range(0f, 5f)]
        public float lookAheadMinSpeed2D = 0.1f;

    }
}
