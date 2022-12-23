using System;
using PluginAPI.Core;

namespace Padoru.Kit.API.Features.Reports
{
    /// <summary>
    /// Данные о репорте
    /// </summary>
    public struct Report
    {
        /// <summary>
        /// Дата и время отправки репорта
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ID отправителя
        /// </summary>
        public int IssuerId { get; set; }

        /// <summary>
        /// ID цели
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Никнейм отправителя
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Никнейм цели
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Инстанциирует запись о репорте
        /// </summary>
        /// <param name="issuer">Отправитель</param>
        /// <param name="target">Цель</param>
        /// <param name="reason">Причина</param>
        public Report(Player issuer, Player target, string reason)
        {
            Date = DateTime.Now;

            IssuerId = issuer.PlayerId;
            IssuerName = issuer.Nickname;

            TargetId = target.PlayerId;
            TargetName = target.Nickname;

            Reason = reason;
        }
    }
}