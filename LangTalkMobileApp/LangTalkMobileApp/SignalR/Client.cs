using LangTalkMobileApp.DTOs;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.SignalR
{
    public class Client
    {
        HubConnection connection;

        public Client()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://10.0.2.2:5254/ChatHub")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        public async Task ConnectAsync()
        {
            if (connection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await connection.StartAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void RegisterHandlers(Action<Message> handleMessage)
        {
            connection.On<Message>("ReceiveMessage", (message) =>
            {
                handleMessage?.Invoke(message);
            });
        }

        public async Task JoinServer(Guid idChat)
        {
            await ConnectAsync();
            await connection.InvokeAsync("JoinChat", idChat.ToString());
        }

        public async Task SendMessage(Guid chatId, Guid userId, string message)
        {
            await ConnectAsync();
            await connection.InvokeAsync("SendMessage", chatId.ToString(), userId.ToString(), message);
        }
    }
}
