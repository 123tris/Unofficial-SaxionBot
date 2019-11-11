using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

class Program
{
    //Call start asynchronously
    static void Main() => new Program().StartAsync().GetAwaiter().GetResult();

    private DiscordSocketClient client;
    private CommandHandler commandHandler;

    public async Task StartAsync()
    {
        //Create client
        client = new DiscordSocketClient();

        //Add log method to callback
        client.Log += message =>
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        };

        //Setup command handler
        commandHandler = new CommandHandler(client);
        await commandHandler.InitAsync();

        //Connect to server
        Console.WriteLine("Inser your token here:\n");
        string token = Console.ReadLine();
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        await Task.Delay(-1);
    }
}