using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option Config = new Option(
            new string[2] { "--config", "-c" },
            "Path to root sitecore.json directory (default: cwd)",
            typeof(string),
            () => Environment.CurrentDirectory
        );
        internal static readonly Option Database = new Option(
            new string[2] { "--database", "-db" },
            "Mention DB name for rebuilding the link DB (default: master)",
            typeof(string),
            () => "master"
        );

        internal static readonly Option JobName = new Option(
            new string[2] { "--job-name", "-j" },
            "Mention DB Task Schedule Name from Listing.",
            typeof(string)
        );
        internal static readonly Option EnvironmentName = new Option(
            new string[2] { "--environment-name", "-n" },
            "Named Sitecore environment to use. Default: 'default'.",
            typeof(string)
        );
        internal static readonly Option Verbose = new Option(
            new string[2] { "--verbose", "-v" },
            "Write some additional diagnostic and performance data",
            typeof(bool),
            () => false
        );
        internal static readonly Option Trace = new Option(
            new string[2] { "--trace", "-t" },
            "Write more additional diagnostic and performance data",
            typeof(bool),
            () => false
        );
    }
}
