namespace Project.EntenEller.Scenes.Performance.Base
{
    public static class TestPerformance
    {
        private const int size = 1000;
        private static int[,] array = new int[size, size];
        
        public static void Hard()
        {
            for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
            {
                array[i, j] = i * j;
            }
        }
        
        public static void Easy()
        {
            for (var i = 0; i < 5; i++)
            for (var j = 0; j < 5; j++)
            {
                array[i, j] = i * j;
            }
        }
    }
}
