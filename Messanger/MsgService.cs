using Messanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Messanger
{
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class MsgService : IMsgService
    {
        int nextID;
        List<UserModel> users = new List<UserModel>();
        public int Connect(string name)
        {
            UserModel user = new UserModel()
            {
                ID = nextID,
                Name = name,
                operationContext = OperationContext.Current
            };
            users.Add(user);
            nextID++;
            SendMsg($"{user.Name} присоеденился к чату!", 0);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user!= null)
            {
                users.Remove(user);
                SendMsg($"{user.Name} покинул чат!", 0);
            }
            
        }

        public void SendMsg(string msg, int id)
        {
            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name;
                }
                answer += ": " + msg;

                item.operationContext.GetCallbackChannel<IMsgServiceCallback>().MsgCallback(answer);
            }
        }
    }
}
