using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models {
    [DataContract]
    public class Chat {
        [DataMember] public SortedDictionary<string, User> UsersDict { get; set; } = new SortedDictionary<string, User>();

        [DataMember] public List<MessageObject> Messages { get; set; } = new List<MessageObject>();

        public bool AddUser(User user) {
            if (DoesEmailExist(user.Email))
                return false;

            UsersDict.Add(user.Email, user);
            return true;
        }

        public void AddMessage(MessageObject messageObject) {
            Messages.Add(messageObject);
        }

        public bool DoesEmailExist(string email) {
            return UsersDict.ContainsKey(email);
        }

        public List<User> GetUserList() {
            return new List<User>(UsersDict.Values);
        }

        public static Chat GetRandomChat(Random random) {
            Chat chat = new Chat();

            int userCount = random.Next(1, 20);
            for (int i = 0; i < userCount; i++) {
                User user = User.RandomUser(random);
                chat.AddUser(user);
            }

            int messageCount = random.Next(1, 20);
            for (int i = 0; i < messageCount; i++) {
                // Shit here!
                chat.AddMessage(MessageObject.RandomMessage(random, chat.GetUserList()));
            }

            return chat;
        }
    }
}