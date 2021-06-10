using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using WebMessages.Models;

namespace WebMessages.Utilities
{
    /// <summary>
    /// Utility to save and load chat object.
    /// </summary>
    public static class SaveLoadChat
    {
        /// <summary>
        /// Save <paramref name="chat"/> into json file on <paramref name="path"/> .
        /// </summary>
        /// <param name="chat"> Chat to save. </param>
        /// <param name="path"> Path to save into. </param>
        public static void Save(this Chat chat, string path)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(Chat));

                using FileStream stream = new FileStream(path, FileMode.Create);
                serializer.WriteObject(stream, chat);
            }
            catch
            {
                // We can't save file. Sad.
            }
        }

        /// <summary>
        /// Load chat object from <paramref name="path"/>.
        /// </summary>
        /// <param name="path"> Path to load from. </param>
        /// <returns> Loaded chat object. </returns>
        public static Chat Load(string path)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(Chat));
                using FileStream stream = new FileStream(path, FileMode.Open);
                return (Chat) serializer.ReadObject(stream);
            }
            catch
            {
                return new Chat();
            }
        }
    }
}