using System;
using System.Collections.Generic;

namespace GuaranteedRateConsoleApp.Models
{
    public partial interface ICollectionItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        string DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the color of the favorite.
        /// </summary>
        /// <value>
        /// The color of the favorite.
        /// </value>
        string FavoriteColor { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        #endregion Public Properties
    }
}