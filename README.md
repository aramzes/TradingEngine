## About the Trading Engine
This project is an educational project to get familiar with C# 9 and .Net Standard 2.1 following Coding Jesus' playlist, Algo Trading Trading Engine Series

The Trading Engine consists of trading engine server that emulates an exchange with ability to connect to multiple clients using gRPC communication.

The Engine holds orderbooks for different instruments that holds the state of each market and a matching engine is used for matching orders when the orderbook state is changed.

## Currently Supported Features
The following features are currently supported.

### Order Types
1. New Order
2. Modify Order
3. Cancel Order

### Order Responses
1. New Order Acknowledgement
2. Modify Order Acknowledgement
3. Cancel Order Acknowledgement
4. Fill

## Building The Project
The following steps will allow you to build and run the engine.

1. Download Visual Studio 2019.
2. Download this repository.
3. Open TradingEngineServer.sln under src/TradingEngineServer
4. Hit F5 to build and run the solution.

## Description
The engine and all trading clients reference a flat file detailing which instruments are supported for trading. Orders submitted from trading clients for instruments not contained in the flat file will be rejected by the engine.

* Trading client connects to the engine via TCP, leveraging gRPC. The trading client now has a private communication channel open between itself and the engine.
* Trading client can submit New Order, Cancel Order, and Modify Order requests, receving a corresponding acknowledgement for each via the gRPC bi-directional stream.
* Upon receipt of a New Order, Cancel Order, or Modify Order, the engine persists the order's information to Cassandra.
* If a trading client's order matches against a resting order, they receive a Fill via the gRPC bi-directional stream.
