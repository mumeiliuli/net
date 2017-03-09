using Emaill.Model.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Service.chat
{
    public class ChatHub:Hub
    {
        private IList<AccountUser> userList = new List<AccountUser>();

        public void SendChat(string id, string name, string message)
        {
            Clients.All.addNewMessageToPage(id, name + " " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), message);
        }

    }
}
