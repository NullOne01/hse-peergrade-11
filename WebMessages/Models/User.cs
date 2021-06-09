using System;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models {
    [DataContract]
    public class User {
        public User(string userName, string email) {
            UserName = userName;
            Email = email;
        }

        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }

        public static User RandomUser(Random random) {
            int wordLength = random.Next(2, 6);
            return new User(random.RandomWord(wordLength), random.RandomEmail());
        }
    }
}