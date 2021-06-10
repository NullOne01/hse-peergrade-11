using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models
{
    /// <summary>
    /// User class.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// User name.
        /// </summary>
        [Required]
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// User email.
        /// </summary>
        [Required]
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Get randomized user.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <returns> Randomized user. </returns>
        public static User RandomUser(Random random)
        {
            int wordLength = random.Next(2, 6);
            return new User(random.RandomWord(wordLength), random.RandomEmail());
        }
    }
}