using System;

namespace GuaranteedRateConsoleApp.Models
{
    public partial class CollectionItem : ICollectionItem
    {
        #region Private Fields

        private DateTime dateValue;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string DateOfBirth
        {
            get
            {
                return dateValue.ToString("M/d/yyyy");
            }
            set { DateTime.TryParse(value, out dateValue); }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the color of the favorite.
        /// </summary>
        /// <value>
        /// The color of the favorite.
        /// </value>
        public string FavoriteColor { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        #endregion Public Properties
    }
}