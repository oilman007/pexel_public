

using Pexel;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void DistanceTest()
        {
            Assert.Equal(2.83, Math.Round(Line2D.Distance(new Point2D(1, 2), new Point2D(3, 4)), 2));
        }
    }
}