using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string username, string message, string time = "")
        {
            time = DateTime.Now.ToString("HH:mm");
            if (message == "加入聊天室")
                //判斷是否是加入聊天室
                await Clients.All.SendAsync("NewJoin", username, message, time);
            else if (message == "離開聊天室")
                //離開聊天
                await Clients.All.SendAsync("Leave", username, message, time);
            else
                //設定所有進入的Client 都收到信息
                await Clients.All.SendAsync("ReceiveMessage", username, message, time);
        }
    }
}
