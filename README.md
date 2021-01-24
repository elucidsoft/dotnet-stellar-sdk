<!-- PROJECT LOGO -->

<br /><p align="center"> <a href="https://github.com/elucidsoft/dotnet-stellar-sdk"><img width="460" height="300" src="https://raw.githubusercontent.com/elucidsoft/dotnet-stellar-sdk/master/.github/images/logo.svg"> </a> <!-- TITLE AND BADGES --> <h3 align="center">dotnet-stellar-sdk</h3> <p align="center"> Stellar API SDK for .NET Core 2.x and .NET Standard 2.0 <br /> <a href="https://ci.appveyor.com/project/elucidsoft/dotnet-stellar-sdk/branch/master"> <img src="https://ci.appveyor.com/api/projects/status/n34q6l3wyar2rq5l/branch/master?svg=true"></a> <a href="https://coveralls.io/github/elucidsoft/dotnet-stellar-sdk?branch=master"> <img src="https://coveralls.io/repos/github/elucidsoft/dotnet-stellar-sdk/badge.svg?branch=master"></a><a href="https://www.codefactor.io/repository/github/elucidsoft/dotnet-stellar-sdk"> <img src="https://www.codefactor.io/repository/github/elucidsoft/dotnet-stellar-sdk/badge"></a> <a href="https://www.nuget.org/packages/stellar-dotnet-sdk"> <img src="https://buildstats.info/nuget/stellar-dotnet-sdk"> </a><br /><br /><!-- USEFUL LINKS--><a href="https://elucidsoft.github.io/dotnet-stellar-sdk/"><strong>Explore the docs »</strong></a> <br /> <br /> <a href="https://github.com/elucidsoft/dotnet-stellar-sdk/issues/new?template=Bug_report.md">Report Bug</a> · <a href="https://github.com/elucidsoft/dotnet-stellar-sdk/issues/new?template=Feature_request.md">Request Feature</a> · <a href="https://github.com/elucidsoft/dotnet-stellar-sdk/security/policy">Report Security Vulnerability</a> </p></p>

<!-- TABLE OF CONTENTS -->

## Table of Contents

-   [About the Project](#about-the-project)
-   [Installation](#installation)
    -   [Visual Studio](#visual-studio)
    -   [JetBrains Rider](#jetbrains-rider)
    -   [Other](#other)
-   [Usage](#usage)
-   [XDR](#xdr)
-   [XDR Generation](#xdr-generation)
-   [Contributors](#contributors)
-   [License](#license)
-   [Acknowledgements](#acknowledgements)

<!-- ABOUT THE PROJECT -->

## About The Project

`dotnet-stellar-sdk` is a **Net Core/Standard** library for communicating with a [Stellar Horizon server](https://github.com/stellar/go/tree/master/services/horizon). It is used for building Stellar apps.

_This project originated as a full port of the official [Java SDK API](https://github.com/stellar/java-stellar-sdk)_

## Installation

The `stellar-dotnet-sdk` library is bundled in a NuGet Package.

-   [NuGet Package](https://www.nuget.org/packages/stellar-dotnet-sdk)

### Visual Studio

-   Using the [console](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell)

    -   Run `Install-Package stellar-dotnet-sdk` in the console.

-   Using the [NuGet Package Manager](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)

    -   Search this package [NuGet Package](https://www.nuget.org/packages/stellar-dotnet-sdk) and install it.

### JetBrains Rider

-   <https://www.jetbrains.com/help/rider/Using_NuGet.html#>

### Other

-   <https://docs.microsoft.com/en-us/nuget/consume-packages/overview-and-workflow#ways-to-install-a-nuget-package>

<!-- USAGE EXAMPLES -->

## Usage

Check the [Tutorials](https://elucidsoft.github.io/dotnet-stellar-sdk/tutorials/index.html) page to get started.

**In case of doubts or issues, you can ask for help here:**

-   [Stellar Stack Exchange](https://stellar.stackexchange.com/)

-   [Keybase Team](https://keybase.io/team/stellar_dotnet)

## XDR

[![NuGet Badge](https://buildstats.info/nuget/stellar-dotnet-sdk-xdr)](https://www.nuget.org/packages/stellar-dotnet-sdk-xdr/)

If you only need the XDR objects in a .NET Standard NuGet package, then you can get those here: <https://www.nuget.org/packages/stellar-dotnet-sdk-xdr/>

## XDR Generation

In order to generate the XDR Files automatically in C# a custom XDR Generator must be used.

You can find the latest working generator here: <https://github.com/fracek/xdrgen/tree/csharp>

You can use that version of xdrgen to regenerate the XDR files from the .x files located from the [source](https://github.com/stellar/stellar-core/tree/master/src/xdr) of the original API SDK for Horizon.

### Example

1. Install custom XDR generator:
   ```
   git clone https://github.com/fracek/xdrgen
   cd xdrgen
   git checkout csharp
   rake install
   ```
2. Regenerate .cs files from .x files:
   ```
   cd dotnet-stellar-sdk/
   xdrgen -o=./stellar-dotnet-sdk-xdr/generated -l=csharp -n=stellar_dotnet_sdk.xdr ./stellar-dotnet-sdk-xdr/*.x
   ```
3. Reformat .cs files using dotnet-format:
   ```
   dotnet format
   ```

<!-- CONTRIBUTORS-->

## Contributors

-   Eric Malamisura (Twitter: [@EricDaCoder](https://twitter.com/EricDaCoder), Keybase: [elucidsoft](https://keybase.io/elucidsoft))
-   Kirbyrawr (Keybase: [Kirbyrawr](https://keybase.io/Kirbyrawr))
-   Michael Monte
-   Francesco Ceccon

<!-- LICENSE -->

## License

`dotnet-stellar-sdk` is licensed under an Apache-2.0 license. See the [LICENSE](https://github.com/elucidsoft/dotnet-stellar-sdk/blob/master/LICENSE.txt) file for details.

<!-- ACKNOWLEDGEMENTS -->

## Acknowledgements

-   Stellar Development Foundation

<!-- Disclaimer -->

<!-- This readme is a modification of https://github.com/othneildrew/Best-README-Template that is licensed under MIT -->
