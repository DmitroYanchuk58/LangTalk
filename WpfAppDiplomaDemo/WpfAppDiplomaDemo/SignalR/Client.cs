using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.SignalR
{
    public class Client
    {
        HubConnection connection;

        public Client()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7067/ChatHub")
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

        public void RegisterHandlers(Action<MessageModel> handleMessage)
        {
            connection.On<MessageModel>("ReceiveMessage", (message) =>
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
