using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WebMessages.Models
{
    /// <summary>
    /// Class chat which holds user and messages information in it.
    /// </summary>
    [DataContract]
    public class Chat
    {
        /// <summary>
        /// Dictionary with users (which sorts keys in lexicographic order).
        /// </summary>
        [DataMember]
        public SortedDictionary<string, User> UsersDict { get; set; } = new SortedDictionary<string, User>();

        /// <summary>
        /// List of messages.
        /// </summary>
        [DataMember]
        public List<MessageObject> Messages { get; set; } = new List<MessageObject>();

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Chat()
        {
        }

        /// <summary>
        /// Add user is possible.
        /// </summary>
        /// <param name="user"> User to add. </param>
        /// <returns> True if user was added. Otherwise false. </returns>
        public bool AddUser(User user)
        {
            if (user == null)
                return false;
            if (DoesEmailExist(user.Email))
                return false;

            UsersDict.Add(user.Email, user);
            return true;
        }

        /// <summary>
        /// Add message if possible.
        /// </summary>
        /// <param name="messageObject"> Message to add. </param>
        /// <returns> True if message was added. Otherwise false. </returns>
        public bool AddMessage(MessageObject messageObject)
        {
            if (messageObject == null)
                return false;
            if (!DoesEmailExist(messageObject.ReceiverId) || !DoesEmailExist(messageObject.SenderId))
                return false;

            Messages.Add(messageObject);
            return true;
        }

        /// <summary>
        /// Are there any users with such email?
        /// </summary>
        /// <param name="email"> Email to check. </param>
        /// <returns> True if email exists. Otherwise false. </returns>
        public bool DoesEmailExist(string email)
        {
            return email != null && UsersDict.ContainsKey(email);
        }

        /// <summary>
        /// Get all users list.
        /// </summary>
        /// <returns> List of users. </returns>
        public List<User> GetUserList()
        {
            return new List<User>(UsersDict.Values);
        }

        /// <summary>
        /// Get <paramref name="limit"/> number of users passing <paramref name="offset"/> elements.
        /// </summary>
        /// <param name="limit"> Number of users to get. </param>
        /// <param name="offset"> Number of users to skip. </param>
        /// <returns> List of users. </returns>
        public List<User> GetUserList(int limit, int offset)
        {
            return UsersDict.Values.Skip(offset).Take(limit).ToList();
        }

        /// <summary>
        /// Get randomized chat.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <returns> Randomized chat. </returns>
        public static Chat GetRandomChat(Random random)
        {
            Chat chat = new Chat();

            int userCount = random.Next(1, 20);
            for (int i = 0; i < userCount; i++)
            {
                User user = User.RandomUser(random);
                chat.AddUser(user);
            }

            int messageCount = random.Next(1, 20);
            List<User> userList = chat.GetUserList();
            for (int i = 0; i < messageCount; i++)
            {
                chat.AddMessage(MessageObject.RandomMessage(random, userList));
            }

            return chat;
        }
    }
}