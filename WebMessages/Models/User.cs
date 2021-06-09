using System;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models {
    /// <summary>
    /// User class.
    /// </summary>
    [DataContract]
    public class User {
        public User(string userName, string email) {
            UserName = userName;
            Email = email;
        }

        public User()
        {
            
        }

        /// <summary>
        /// User name.
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// User email.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Get randomized user.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <returns> Randomized user. </returns>
        public static User RandomUser(Random random) {
            int wordLength = random.Next(2, 6);
            return new User(random.RandomWord(wordLength), random.RandomEmail());
        }
    }
}