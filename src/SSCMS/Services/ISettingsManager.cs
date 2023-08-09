﻿using System;
using Datory;
using Microsoft.Extensions.Configuration;

namespace SSCMS.Services
{
    public partial interface ISettingsManager
    {
        IConfiguration Configuration { get; set; }
        string ContentRootPath { get; }
        string WebRootPath { get; }
        string Version { get; }
        string FrameworkDescription { get; }
        string OSArchitecture { get; set; }
        string OSDescription { get; }
        bool Containerized { get; }
        int CPUCores { get; }
        bool IsProtectData { get; }
        bool IsSafeMode { get; }
        bool IsDisablePlugins { get; }
        string SecurityKey { get; }
        DatabaseType DatabaseType { get; }
        string DatabaseConnectionString { get; }
        IDatabase Database { get; }
        string RedisConnectionString { get; }
        int MaxSites { get; }
        IRedis Redis { get; }
        public string AdminRestrictionHost { get; }
        public string[] AdminRestrictionAllowList { get; }
        public string[] AdminRestrictionBlockList { get; }
<<<<<<< HEAD
        string Encrypt(string inputString, string securityKey = null);
        string Decrypt(string inputString, string securityKey = null);
        void SaveSettings(bool isProtectData, bool isSafeMode, bool isDisablePlugins, DatabaseType databaseType, string databaseConnectionString, string redisConnectionString, string adminRestrictionHost, string[] adminRestrictionAllowList, string[] adminRestrictionBlockList);
=======
        public bool CorsIsOrigins { get; }
        public string[] CorsOrigins { get; }
        string Encrypt(string inputString, string securityKey = null);
        string Decrypt(string inputString, string securityKey = null);
        void SaveSettings(bool isProtectData, bool isSafeMode, bool isDisablePlugins, DatabaseType databaseType, string databaseConnectionString, string redisConnectionString, string adminRestrictionHost, string[] adminRestrictionAllowList, string[] adminRestrictionBlockList, bool corsIsOrigins, string[] corsOrigins);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        IServiceProvider BuildServiceProvider();
        void Reload();
    }
}
