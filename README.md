

# Hackathon Submission Entry form

## Team name
Sitecore Warriors

## Category
2. Extend the Sitecore Command Line Interface (CLI) plugin 

## Description
This Sitecore Job plugin will enable admins to 
  
  - View currently running, queued and finished jobs. Also lists configured Sitecore Database jobs.
  - Execute Sitecore Database jobs on-demand
  - Rebuild the Link database

## Video link

[https://www.youtube.com/watch?v=FfsEiBhCEmc](https://www.youtube.com/watch?v=FfsEiBhCEmc)



## Pre-requisites and Dependencies

This module depends on Sitecore CLI. 

  - Sitecore Management Services
  - Sitecore CLI

## Installation instructions

 1. Bring up a Sitecore instance with Sitecore CLI. Please make sure to [install Sitecore Management Services](https://doc.sitecore.com/xp/en/developers/102/developer-tools/sitecore-management-services.html "Sitecore Management Services"). 
 2. Download the [`Job` Management Services package](https://github.com/Sitecore-Hackathon/2022-Sitecore-Warriors/tree/main/Sitecore_Packages)  file.   
2.  On the Sitecore Launchpad, click  Control Panel,  Install a package. Then follow the Installation Wizard to install the  `Job Management Services package`  package file.
 4. Go to your project folder in a terminal with administrator privileges.
 5. Install the `Job` plugin:
  `dotnet sitecore plugin add -n SitecoreWarriors.DevEx.Extensibility.Jobs`
You can check the installed plugins using the `dotnet sitecore plugin list` command: `List of plugins:
SitecoreWarriors.DevEx.Extensibility.Jobs v.4.1.1`

 4. To verify that the Sitecore CLI `Job` plugin is installed, go to your project folder in a terminal and type `dotnet sitecore job -h`, you will get the list of available commands for `Job` plugin.

## Usage instructions

You can use the command as follows:

    dotnet sitecore job [subcommand] [options]
### Subcommands
You can use the following subcommands:

 1. list - Get all jobs list (running, queued, finished and db task jobs). Db task can be started on-demand.
 2. rebuildlinkdb - Start rebuilding a link db.
 3. start - Start a db task.
### Options
You can use the following options with the  `list`  subcommand:

  *-c, --config (config)    Path to root sitecore.json directory (default: cwd)
  
  -v, --verbose            Write some additional diagnostic and performance data
  
  -t, --trace              Write more additional diagnostic and performance data
  
  -?, -h, --help           Show help and usage information*


You can use the following options with the  `start`  subcommand:

  *-c, --config (config)        Path to root sitecore.json directory (default: cwd)
  
  -j, --job-name (job-name)    Mention DB Task Schedule Name from Listing.
  
  -v, --verbose                Write some additional diagnostic and performance data
  
  -t, --trace                  Write more additional diagnostic and performance data
  
  -?, -h, --help               Show help and usage information*

  
You can use the following options with the  `rebuildlinkdb`  subcommand:

  *-c, --config (config)         Path to root sitecore.json directory (default: cwd)
  
  -db, --database (database)    Mention DB name for rebuilding the link DB (default: master)
  
  -v, --verbose                 Write some additional diagnostic and performance data
  
  -t, --trace                   Write more additional diagnostic and performance data
  
  -?, -h, --help                Show help and usage information*
