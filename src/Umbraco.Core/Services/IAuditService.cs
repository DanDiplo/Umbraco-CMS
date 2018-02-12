using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Umbraco.Core.Persistence.Querying;

namespace Umbraco.Core.Services
{
    /// <summary>
    /// Represents a service for handling audit.
    /// </summary>
    public interface IAuditService : IService
    {
        void Add(AuditType type, string comment, int userId, int objectId);

        /// <summary>
        /// Returns paged items in the audit trail for a given entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="orderDirection">
        /// By default this will always be ordered descending (newest first)
        /// </param>
        /// <param name="auditTypeFilter">
        /// Since we currently do not have enum support with our expression parser, we cannot query on AuditType in the query or the custom filter
        /// so we need to do that here
        /// </param>
        /// <param name="customFilter">
        /// Optional filter to be applied
        /// </param>
        /// <returns></returns>
        IEnumerable<IAuditItem> GetPagedItemsByEntity(int entityId, long pageIndex, int pageSize, out long totalRecords,
            Direction orderDirection = Direction.Descending,
            AuditType[] auditTypeFilter = null,
            IQuery<IAuditItem> customFilter = null);

        /// <summary>
        /// Returns paged items in the audit trail for a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="orderDirection">
        /// By default this will always be ordered descending (newest first)
        /// </param>
        /// <param name="auditTypeFilter">
        /// Since we currently do not have enum support with our expression parser, we cannot query on AuditType in the query or the custom filter
        /// so we need to do that here
        /// </param>
        /// <param name="customFilter">
        /// Optional filter to be applied
        /// </param>
        /// <returns></returns>
        IEnumerable<IAuditItem> GetPagedItemsByUser(int userId, long pageIndex, int pageSize, out long totalRecords,
            Direction orderDirection = Direction.Descending,
            AuditType[] auditTypeFilter = null,
            IQuery<IAuditItem> customFilter = null);

        /// <summary>
        /// Writes an audit entry for an audited event.
        /// </summary>
        /// <param name="performingUserId">The identifier of the user triggering the audited event.</param>
        /// <param name="perfomingDetails">Free-form details about the user triggering the audited event.</param>
        /// <param name="performingIp">The IP address or the request triggering the audited event.</param>
        /// <param name="eventDate">The date and time of the audited event.</param>
        /// <param name="affectedUserId">The identifier of the user affected by the audited event.</param>
        /// <param name="affectedDetails">Free-form details about the entity affected by the audited event.</param>
        /// <param name="eventType">
        /// The type of the audited event - must contain only alphanumeric chars, hyphens and at least one '/' defining categories
        /// </param>
        /// <param name="eventDetails">Free-form details about the audited event.</param>
        IAuditEntry Write(int performingUserId, string perfomingDetails, string performingIp, DateTime eventDate, int affectedUserId, string affectedDetails, string eventType, string eventDetails);

        /// <summary>
        /// Retrieves audit entries.
        /// </summary>
        IEnumerable<IAuditEntry> Get(); // fixme missing querying options

        /// <summary>
        /// Retrieves audit entries.
        /// </summary>
        /// <remarks>Entries are ordered by event date, most recent first.</remarks>
        IEnumerable<IAuditEntry> GetPage(long pageIndex, int pageCount, out long records); // fixme missing querying options
    }
}
