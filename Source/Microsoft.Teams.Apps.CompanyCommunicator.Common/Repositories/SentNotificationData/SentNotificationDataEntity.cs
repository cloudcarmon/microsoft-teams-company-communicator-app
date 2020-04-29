﻿// <copyright file="SentNotificationDataEntity.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.CompanyCommunicator.Common.Repositories.SentNotificationData
{
    using System;
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// Sent notification entity class.
    /// This entity holds all of the information about a recipient and the results for
    /// a notification having been sent to that recipient.
    /// </summary>
    public class SentNotificationDataEntity : TableEntity
    {
        /// <summary>
        /// This value is to be used when the entity is first initialized and stored and does
        /// not yet have a valid status code from a response for an attempt at sending the
        /// notification to the recipient.
        /// </summary>
        public static readonly int InitializationStatusCode = 0;

        /// <summary>
        /// String indicating the recipient type for the given notification was a user.
        /// </summary>
        public static readonly string UserRecipientType = "User";

        /// <summary>
        /// String indicating the recipient type for the given notification was a team.
        /// </summary>
        public static readonly string TeamRecipientType = "Team";

        /// <summary>
        /// String indicating success of sending the notification to the recipient.
        /// </summary>
        public static readonly string Succeeded = "Succeeded";

        /// <summary>
        /// String indicating a failure response was received when sending the notification to
        /// the recipient.
        /// </summary>
        public static readonly string Failed = "Failed";

        /// <summary>
        /// [Deprecated] String indicationg sending the notification to the recipient was throttled
        /// and not sent successfully.
        /// </summary>
        public static readonly string Throttled = "Throttled";

        /// <summary>
        /// String indicating sending the current notification resulted
        /// in an exception. Because of this, this string will be stored in the repository
        /// until a more final state is reached by attempting to send the notification again.
        /// </summary>
        public static readonly string Continued = "Continued";

        /// <summary>
        /// Gets or sets a value indicating which type of recipient the notification was sent to
        /// using the recipient type strings at the top of this class.
        /// </summary>
        public string RecipientType { get; set; }

        /// <summary>
        /// Gets or sets the recipient's unique identifier.
        ///     If the recipient is a user, this should be the AAD Id.
        ///     If the recipient is a team, this should be the team Id.
        /// </summary>
        public string RecipientId { get; set; }

        /// <summary>
        /// Gets or sets the total number of throttle responses the bot received when trying
        /// to send the notification to this recipient.
        /// Note: This does not include throttle responses received when creating the conversation.
        /// This total only represents throttle responses received when actually calling the send API.
        /// Note: This count may not include every throttled response the bot has ever received when
        /// attempting to send the notification to this recipient. If the queue message is added back
        /// to the queue to retry and the results of the attempt are not stored in the Sent Notification
        /// data table for that attempt, then those results are lost and are not stored here e.g. if
        /// the bot is put in a throttled state.
        /// </summary>
        public int TotalNumberOfSendThrottles { get; set; }

        /// <summary>
        /// Gets or sets the DateTime the last recorded attempt at sending the notification to this
        /// recipient was completed.
        /// </summary>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the status code is from the create conversation call.
        /// </summary>
        public bool IsStatusCodeFromCreateConversation { get; set; }

        /// <summary>
        /// Gets or sets the last recorded response status code received by the bot when attempting to
        /// send the notification to this recipient.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a comma separated list representing all of the status code responses received when trying
        /// to send the notification to the recipient. These results can include success, failure, and throttle
        /// status codes. This list can also include a '-1' as a result if the function throws an overall exception
        /// when attempting to process the queue message.
        /// Note: This value may not include every status code response the bot has ever received when
        /// attempting to send the notification to this recipient. If the queue message is added back to
        /// the queue to retry and the results of the attempt are not stored in the Sent Notification data
        /// table for that attempt, then those results are lost and are not stored here e.g. if the bot is put
        /// in a throttled state (in that scenario all response codes that are missing should primarily be
        /// throttle responses).
        /// </summary>
        public string AllSendStatusCodes { get; set; }

        /// <summary>
        /// Gets or sets the number of times an Azure Function instance attempted to send the notification
        /// to the recipient and stored a final result.
        /// Note: This should only ever be one. If it is more than one, it is possible the recipient incorrectly
        /// received multiple, duplicate notifications - check AllSendStatusCodes.
        /// Note: If the function is triggered by a queue message but the processing is skipped, e.g. the
        /// system is in a throttled state or it is a duplicate message, then that is not counted as an attempt
        /// in this count.
        /// </summary>
        public int NumberOfFunctionAttemptsToSend { get; set; }

        /// <summary>
        /// Gets or sets the summarized delivery status for the notification to this recipient using the
        /// status strings at the top of this class.
        /// </summary>
        public string DeliveryStatus { get; set; }

        /// <summary>
        /// Gets or sets the conversation id for the recipient.
        /// </summary>
        public string ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the service url for the recipient.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the tenant id for the recipient.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the user id for the recipient.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the error message for the last recorded error the bot encountered
        /// when attempting to process the queue message. If a request
        /// is retried and eventually is successful, this field will still be filled
        /// with data about the last error encountered.
        /// Note: This is a record for the last error encountered. If multiple
        /// errors are encountered when attempting to process the queue message
        /// only the final one will be stored here.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
