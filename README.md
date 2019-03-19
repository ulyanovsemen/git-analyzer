# Git Analyzer

Analyze git history with simple command line for statistics.

### Build

Visual Studio or command line

      dotnet build

### Run sample:

      dotnet git-an.dll c:\path-to-my-git-repo

### Notes
authors.txt file must contain authors for deduplication, separated by comma.

You also can specify dates for analyzing (default is 2018 year) in 'dd.MM.yyyy' format

      dotnet git-an.dll c:\path-to-my-git-repo 08.01.2018 30.12.2018
