using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public class MessageManager
    {
        public static Message Create(Message message)
        {
            using (var db = new MakerContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();

                return message;
            }
        }

        public static List<Message> Read()
        {
            using (var db = new MakerContext())
            {
                var messages = db.Messages.ToList();
                return messages;
            }
        }

        public static void Delete(Message message)
        {
            using (var db = new MakerContext())
            {
                db.Messages.Attach(message);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }
    }
}