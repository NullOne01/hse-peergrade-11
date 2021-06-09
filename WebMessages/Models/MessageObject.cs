using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebMessages.Utilities;

namespace WebMessages.Models {
    [DataContract]
    public class MessageObject {
        [DataMember]
        public string Subject { get; set; }
        
        // Капец, нельзя назвать класс Message из-за этого свойства :c
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string SenderId { get; set; }
        [DataMember]
        public string ReceiverId { get; set; }

        public MessageObject(string subject, string message, string senderId, string receiverId) {
            Subject = subject;
            Message = message;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
        
        public static MessageObject RandomMessage(Random random, List<User> users) {
            return new MessageObject(random.RandomString(10),
                random.RandomString(30),
                users[random.Next(users.Count)].Email,
                users[random.Next(users.Count)].Email);
        }
    }
}