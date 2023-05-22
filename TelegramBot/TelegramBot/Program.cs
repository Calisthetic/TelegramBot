using Newtonsoft.Json;
using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using TelegramBot;
using TelegramBot.Config;

namespace TelegramBot // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main()
        {
            Bot.Start();
            Console.ReadLine();
        }
        
    }
}