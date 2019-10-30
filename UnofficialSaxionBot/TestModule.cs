using System.Threading.Tasks;
using Discord.Commands;

public class TestModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("It will pong, you have been warned")]
    public Task PongAsync() => ReplyAsync("pong!");
}
