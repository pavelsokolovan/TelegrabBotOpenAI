using Serilog;
using TelegrabBotOpenAI.DataProviders;
using TelegrabBotOpenAI.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegrabBotOpenAI
{
    public class Bot : IBot
    {
        private readonly TelegramBotClient botClient;
        private readonly IOpenAI openAIAPI;
        private readonly IDataProvider dataProvider;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly ILogger logger;

        public Bot(IOpenAI openAI,
                    IDataProvider dataProvider,
                    ISettings settings,
                    ILogger logger)
        {
            this.botClient = new TelegramBotClient(settings.BotToken);
            this.openAIAPI = openAI;
            this.dataProvider = dataProvider;
            this.cancellationTokenSource = new CancellationTokenSource();
            this.logger = logger;
        }

        public void Start()
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationTokenSource.Token
            );
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
            {
                return;
            }
            if (message.Text is not { } messageText)
            {
                return;
            }

            LogMessageFromChat(message);

            string responce;
            switch (update.Message.Text)
            {
                case "/start":
                    {
                        responce = "Hello! This bot provides openIP complitions on your message.";
                    }
                    break;
                default:
                    CompletionSettingsEntity settingsEntity = dataProvider.ReadCompletionSettings(message.Chat.Id);
                    CompletionSettings settings = CompletionSettings.CreateModel(settingsEntity);
                    responce = await openAIAPI.GetComplitionAsync(messageText, settings);
                    break;
            }

            await SendTextMessage(message.Chat.Id, responce);

            logger.Information($"Respoce from bot:\r\n{responce}");
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task SendTextMessage(long chatId, string text)
        {
            await botClient.SendTextMessageAsync(chatId: chatId, text: text);
        }

        private void LogMessageFromChat(Message message)
        {
            logger.Information(string.Format("From: {0} {1}\r\nCaht ID: {2}\r\nMessage: {3}",
                            message.Chat.FirstName, message.Chat.LastName,
                            message.Chat.Id,
                            message.Text));
        }
    }
}
