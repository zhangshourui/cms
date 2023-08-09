﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;
using SSCMS.Enums;
using SSCMS.Models;

namespace SSCMS.Repositories
{
    public interface IStatRepository : IRepository
    {
        Task AddCountAsync(StatType statType);

        Task AddCountAsync(StatType statType, int siteId);

        Task AddCountAsync(StatType statType, int siteId, int adminId);

        Task<List<Stat>> GetStatsAsync(DateTime lowerDate, DateTime higherDate,
            StatType statType);

        Task<List<Stat>> GetStatsAsync(DateTime lowerDate, DateTime higherDate,
            StatType statType, int siteId);

        Task<List<Stat>> GetStatsAsync(DateTime lowerDate, DateTime higherDate,
            StatType statType, int siteId, int adminId);

        Task<List<Stat>> GetStatsWithAdminIdAsync(DateTime lowerDate, DateTime higherDate,
            StatType statType, int siteId);
<<<<<<< HEAD
=======

        Task DeleteAllAsync(int siteId);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
    }
}