namespace Padoru.Donate.API.Enums
{
    /// <summary>
    /// Тип привилегии
    /// </summary>
    public enum PrivilegeType
    {
        /// <summary>
        /// Резервный слот
        /// </summary>
        ReservedSlot = 1,

        /// <summary>
        /// Плашка
        /// </summary>
        Badge = 2,

        /// <summary>
        /// Gold Patron
        /// </summary>
        PatronTier1 = 3,

        /// <summary>
        /// Diamond Patron
        /// </summary>
        PatronTier2 = 4,

        /// <summary>
        /// Platinum Patron
        /// </summary>
        PatronTier3 = 5
    }
}