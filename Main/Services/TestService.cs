namespace TwseDataHub.Services
{
    /// <summary>
    /// Represents the interface for the test service.
    /// </summary>
    public interface ITestService
    {
        /// <summary>
        /// Gets a test number.
        /// </summary>
        /// <returns>The test number.</returns>
        int GetTestNumber();
    }

    /// <summary>
    /// Represents the implementation of the test service.
    /// </summary>
    public class TestService : ITestService
    {
        /// <summary>
        /// Gets a test number.
        /// </summary>
        /// <returns>The test number.</returns>
        public int GetTestNumber()
        {
            var rnd = new Random();
            return rnd.Next(0, 10);
        }
    }
}
