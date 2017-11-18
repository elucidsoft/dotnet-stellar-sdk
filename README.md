# dotnetcore-stellar-sdk
Stellar API SDK for .NET Core 2.x

[![Build Status](https://travis-ci.org/elucidsoft/dotnetcore-stellar-sdk.svg?branch=master)](https://travis-ci.org/elucidsoft/dotnetcore-stellar-sdk) [![NuGet Badge](https://buildstats.info/nuget/stellar-dotnetcore-sdk)](www.nuget.org/packages/stellar-dotnetcore-sdk/)

The .NET Core Stellar Sdk library provides APIs to build transactions and connect to [Horizon](https://github.com/stellar/horizon).

Read more about [Stellar](https://www.stellar.org/)

This project is a full port of the official [Java SDK API](https://github.com/stellar/java-stellar-sdk).  It is fully functional and all of the original Java Unit Tests were also ported and are passing.  

## Quick Start
To install using Nuget run `Install-Package stellar-dotnetcore-sdk -Version 1.0.1` or install the Nuget package from Visual Studio.

## Capabilities
- [Accounts](https://www.stellar.org/developers/guides/concepts/accounts.html) - Create, Query, Payment, Path Payment, Manage Offer, Create Passive Offer, Set Options, Change Trust, Allow Trust, Account Merge, Inflation, Manage Data, Paging (Limit, Cursor, Sorting)
- [Assets](https://www.stellar.org/developers/guides/concepts/assets.html) - CreateNonNativeAsset types (AlphaNum4, AlphaNum12), XDR Conversion
- [Distributed Exchange](https://www.stellar.org/developers/guides/concepts/exchange.html) - Orderbook, Passive Offers
- [Federation](https://www.stellar.org/developers/learn/concepts/federation.html)
- [Inflation](https://www.stellar.org/developers/guides/concepts/inflation.html) - Inflation Operation
- [Ledger](https://www.stellar.org/developers/horizon/reference/resources/ledger.html) Create, Query, Paging (Limit, Cursor, Sorting)
- [Multisignature](https://www.stellar.org/developers/guides/concepts/multi-sig.html) - Allows multiple signature per transaction, Thresholds, ed25519 signature scheme, Transaction Signing, Allow Trust, Set Options, Hash, Envelopes
- [Stellar.toml](https://www.stellar.org/developers/guides/concepts/stellar-toml.html) - Supports the Stellar TOML format.
- [Testnet](https://www.stellar.org/developers/guides/concepts/test-net.html) - Supports Network switching from Public to Testnet
- [Transactions](https://www.stellar.org/developers/guides/concepts/transactions.html) - Source Accounmt, Fee, Sequence Number, Supported Operations (see below), Signatures (multiple), Memo (Text, Id, Hash, Return)
- [XDR](https://www.stellar.org/developers/guides/concepts/xdr.html) - Full XDR Support, Custom XDRGenerator, Supports .x files

### Supported Operations
- [Create Account](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#create-account)
- [Payment](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#payment)
- [Path Payment](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#path-payment)
- [Manage Offer](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#manage-offer)
- [Create Passive Offer](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#create-passive-offer)
- [Set Options](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#set-options)
- [Change Trust](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#change-trust)
- [Allow Trust](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#allow-trust)
- [Account Merge](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#account-merge)
- [Inflation](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#inflation)
- [Manage Data](https://www.stellar.org/developers/guides/concepts/list-of-operations.html#manage-data)
- [Paging](https://www.stellar.org/developers/horizon/reference/paging.html) (Limit, Cursor, Sorting)
- [Rate Limiting](https://www.stellar.org/developers/horizon/reference/rate-limiting.html) (Limit, Remaining, Reset)
- [Response Format](https://www.stellar.org/developers/horizon/reference/responses.html) (Links, Embedded Resourced, Attributes, Pages, Streaming)

## Documentation
Read the API [Reference Documentation](https://elucidsoft.github.io/dotnetcore-stellar-sdk/) for more information about the API.  For more guidance Stellar.org has documentation that is specific to their [Javascript API](https://www.stellar.org/developers/js-stellar-sdk/reference/) but usage is very similar.

## Basic Usage
For some examples on how to use this library, take a look at the [Get Started docs in the developers site](https://www.stellar.org/developers/guides/get-started/create-account.html).

## Demo Application
In the root of the solution there is a console application called TestConsole, it connects to the Horizon TestNet and pulls down some data. The TestNet can be cleared at any moment so the keys it uses may not be valid.  You can use the [Stellar Laboratory](https://www.stellar.org/laboratory/) to setup an account on TestNet and to play around with data between the TestNet and the API.  You can also use the API to create an account, and Laboratory to validate the results, vice versa!  

## License
dotnetcore-stellar-sdk is licensed under an Apache-2.0 license. See the [LICENSE](https://github.com/elucidsoft/dotnetcore-stellar-sdk/blob/master/LICENSE.txt) file for details.
