using System;

namespace MediaMonkey
{
    public class AppConfig
    {
        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string ContentPath { get; set; }
        public string FTPUrl { get; set; }
        public string LocalUrl { get; set; }
    }
}