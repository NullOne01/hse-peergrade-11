using System;
using System.Linq;

namespace WebMessages.Utilities
{
    /// <summary>
    /// Extensions for random class.
    /// </summary>
    public static class RandomUtility
    {
        /// <summary>
        /// Get random string of chosen length. String generates from characters: <paramref name="chars"/>
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <param name="length"> Chosen length. </param>
        /// <param name="chars"> Characters to generate word from. </param>
        /// <returns> Randomized string. </returns>
        public static string RandomString(this Random random, int length,
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Get random word in upper-case style.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <param name="length"> Chosen length. </param>
        /// <returns> Randomized word. </returns>
        public static string RandomWord(this Random random, int length)
        {
            return random.RandomString(length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        /// <summary>
        /// Get random email.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <returns> Randomized email. </returns>
        public static string RandomEmail(this Random random)
        {
            return random.RandomString(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") +
                   "@" +
                   random.RandomString(5, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") +
                   ".com";
        }
    }
}