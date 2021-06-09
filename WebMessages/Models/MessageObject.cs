using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models {
    /// <summary>
    /// Message object.
    /// </summary>
    [DataContract]
    public class MessageObject {
        /// <summary>
        /// Subject of the message.
        /// </summary>
        [DataMember]
        public string Subject { get; set; }
        
        // Капец, нельзя назвать класс Message из-за этого свойства :c
        /// <summary>
        /// Text of the message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }
        /// <summary>
        /// Id of sender user.
        /// </summary>
        [DataMember]
        public string SenderId { get; set; }
        /// <summary>
        /// Id of receiver user.
        /// </summary>
        [DataMember]
        public string ReceiverId { get; set; }

        public MessageObject(string subject, string message, string senderId, string receiverId) {
            Subject = subject;
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        public MessageObject()
        {
            
        }
        
        /// <summary>
        /// Get randomized message.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <param name="users"> Users. </param>
        /// <returns> Randomized message. </returns>
        public static MessageObject RandomMessage(Random random, List<User> users) {
            return new MessageObject(random.RandomString(10),
                random.RandomString(30),
                users[random.Next(users.Count)].Email,
                users[random.Next(users.Count)].Email);
        }
    }
}