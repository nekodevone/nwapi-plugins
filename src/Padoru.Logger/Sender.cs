using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading;
using Padoru.API.Features;
using Padoru.Logger.Structs;
using PluginAPI.Core;
using UnityEngine;
using Utf8Json;

namespace Padoru.Logger
{
    public class Sender
    {
        /// <summary>
        /// Очередь сообщений
        /// </summary>
        private static readonly ConcurrentQueue<string> Queue = new();

        /// <summary>
        /// Конструктор
        /// </summary>
        public Sender()
        {
            new Thread(LoopSend).Start();
        }

        /// <summary>
        /// Добавление сообщение для отправки
        /// </summary>
        /// <param name="content">Сообщение</param>
        public void AddToQuene(string content)
        {
            Queue.Enqueue(content);
        }

        /// <summary>
        /// Получить текущие время
        /// </summary>
        /// <returns>Текущие время</returns>
        private string GetCurrentTime()
        {
            var currentTime = DateTimeOffset.Now;
            var timestamp = currentTime.ToString("HH:mm:ss");
            var ticks = currentTime.ToUnixTimeSeconds();

            return $"`[{timestamp}]` <t:{ticks}:T>";
        }

        /// <summary>
        /// Зацикленная отправка сообщений каждые 0.5 секунд из <see cref="Queue"/>
        /// </summary>
        private void LoopSend()
        {
            for (;;)
            {
                try
                {
                    if (Queue.TryDequeue(out var message))
                    {
                        while (message.Length > 0)
                        {
                            var cropped = message.Substring(0, Mathf.Clamp(message.Length, 0, 1900));
                            var content = JsonSerializer.ToJsonString(new WebhookParams($"{GetCurrentTime()} - {cropped}"));

                            Http.Client.PostAsync(Plugin.Instance.Config.WebhookUrl,
                                new StringContent(content, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                            message = message.Substring(Mathf.Clamp(1901, 0, message.Length));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning($"Что то пошло не так, попробуем через 5 секунд\n{ex}");
                    Thread.Sleep(5000);
                }

                Thread.Sleep(500);
            }
        }
    }
}