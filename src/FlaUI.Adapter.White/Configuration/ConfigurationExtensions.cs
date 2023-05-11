using System;

namespace TestStack.White.Configuration
{
    /// <summary>
    /// Used to ensure backwards compatibility
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static TimeSpan FindWindowTimeout()
        {
            return TimeSpan.FromMilliseconds(30000);
        }
    }
}