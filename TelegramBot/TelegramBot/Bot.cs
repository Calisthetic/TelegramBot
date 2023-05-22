using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.Config;

namespace TelegramBot
{
    internal class Bot
    {
        public static async void Start()
        {
            try { 
                var json = string.Empty;
                var fs =  System.IO.File.OpenRead("config.json");

                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                    json = await sr.ReadToEndAsync();
                ConfigJson config = JsonConvert.DeserializeObject<ConfigJson>(json);

                TelegramBotClient client = new TelegramBotClient(config.token);
                client.StartReceiving(Update, Error);

                var botHimself = await client.GetMeAsync();

                Console.WriteLine($"Bot {botHimself.Username} started!");
            }
            catch (Exception ex)
            {
                LogError(ex.Message);
            }
        }
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken arg3)
        {
            if (update.Message != null)
            {
                var message = update.Message;
                var chatId = update.Message.Chat.Id;

                if (!string.IsNullOrEmpty(update.Message.Text))
                {
                    Console.WriteLine(
                        $"{message.From.FirstName} sent message {message.MessageId} " +
                        $"to chat {message.Chat.Id} at {message.Date.ToLocalTime()}.");
                    Console.WriteLine($"{message.Chat.Username} | {message.Text}");
                    if (message.Text.ToLower().Contains("hi"))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "**Hello!**");
                        return;
                    }
                }
                if (message.Photo != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Hello!");
                    return;
                }
                if (message.Photo != null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Hello!");
                    return;
                }
            }
            else
                LogError("Can't get data...");
        }
        async static Task<Task> Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        public static void LogError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
