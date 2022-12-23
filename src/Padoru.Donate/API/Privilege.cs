using System;
using System.Runtime.Serialization;
using Padoru.Donate.API.Enums;

namespace Padoru.Donate.API
{
    /// <summary>
    /// Привилегия
    /// </summary>
    public class Privilege
    {
        /// <summary>
        /// Тип привилегии
        /// </summary>
        [DataMember(Name = "type")]
        public PrivilegeType Type { get; set; }

        /// <summary>
        /// Данные привилегии
        /// </summary>
        [DataMember(Name = "data")]
        public object Data { get; set; }

        /// <summary>
        /// Дата создания привилегии
        /// </summary>
        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата окончания привилегии
        /// </summary>
        [DataMember(Name = "expiresAt")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// <see cref="TimeSpan"/> до окончания привилегии
        /// </summary>
        [IgnoreDataMember]
        public TimeSpan ExpiresIn => ExpiresAt - DateTime.Now;
        
        /// <summary>
        /// Длительность привилегии в днях
        /// </summary>
        [IgnoreDataMember]
        public ushort Duration => (ushort)(ExpiresAt - CreatedAt).TotalDays;
    }
}