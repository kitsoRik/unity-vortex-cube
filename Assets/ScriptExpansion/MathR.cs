namespace kitsoRik
{
    public static class MathR
    {
        public static float PercentLerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static bool IsFloating(float a)
        {
            return a != (int)a;
        }

        public static bool IsFloating(double a)
        {
            return a != (int)a;
        }
    }
}
