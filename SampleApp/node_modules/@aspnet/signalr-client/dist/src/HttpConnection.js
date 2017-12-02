"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const Transports_1 = require("./Transports");
const HttpClient_1 = require("./HttpClient");
const ILogger_1 = require("./ILogger");
const Loggers_1 = require("./Loggers");
class HttpConnection {
    constructor(url, options = {}) {
        this.features = {};
        this.logger = Loggers_1.LoggerFactory.createLogger(options.logging);
        this.url = this.resolveUrl(url);
        options = options || {};
        this.httpClient = options.httpClient || new HttpClient_1.HttpClient();
        this.connectionState = 0 /* Initial */;
        this.options = options;
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () {
            if (this.connectionState != 0 /* Initial */) {
                return Promise.reject(new Error("Cannot start a connection that is not in the 'Initial' state."));
            }
            this.connectionState = 1 /* Connecting */;
            this.startPromise = this.startInternal();
            return this.startPromise;
        });
    }
    startInternal() {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                let negotiatePayload = yield this.httpClient.options(this.url);
                let negotiateResponse = JSON.parse(negotiatePayload);
                this.connectionId = negotiateResponse.connectionId;
                // the user tries to stop the the connection when it is being started
                if (this.connectionState == 3 /* Disconnected */) {
                    return;
                }
                this.url += (this.url.indexOf("?") == -1 ? "?" : "&") + `id=${this.connectionId}`;
                this.transport = this.createTransport(this.options.transport, negotiateResponse.availableTransports);
                this.transport.onDataReceived = this.onDataReceived;
                this.transport.onClosed = e => this.stopConnection(true, e);
                let requestedTransferMode = this.features.transferMode === 2 /* Binary */
                    ? 2 /* Binary */
                    : 1 /* Text */;
                this.features.transferMode = yield this.transport.connect(this.url, requestedTransferMode);
                // only change the state if we were connecting to not overwrite
                // the state if the connection is already marked as Disconnected
                this.changeState(1 /* Connecting */, 2 /* Connected */);
            }
            catch (e) {
                this.logger.log(ILogger_1.LogLevel.Error, "Failed to start the connection. " + e);
                this.connectionState = 3 /* Disconnected */;
                this.transport = null;
                throw e;
            }
            ;
        });
    }
    createTransport(transport, availableTransports) {
        if (!transport && availableTransports.length > 0) {
            transport = Transports_1.TransportType[availableTransports[0]];
        }
        if (transport === Transports_1.TransportType.WebSockets && availableTransports.indexOf(Transports_1.TransportType[transport]) >= 0) {
            return new Transports_1.WebSocketTransport(this.logger);
        }
        if (transport === Transports_1.TransportType.ServerSentEvents && availableTransports.indexOf(Transports_1.TransportType[transport]) >= 0) {
            return new Transports_1.ServerSentEventsTransport(this.httpClient, this.logger);
        }
        if (transport === Transports_1.TransportType.LongPolling && availableTransports.indexOf(Transports_1.TransportType[transport]) >= 0) {
            return new Transports_1.LongPollingTransport(this.httpClient, this.logger);
        }
        if (this.isITransport(transport)) {
            return transport;
        }
        throw new Error("No available transports found.");
    }
    isITransport(transport) {
        return typeof (transport) === "object" && "connect" in transport;
    }
    changeState(from, to) {
        if (this.connectionState == from) {
            this.connectionState = to;
            return true;
        }
        return false;
    }
    send(data) {
        if (this.connectionState != 2 /* Connected */) {
            throw new Error("Cannot send data if the connection is not in the 'Connected' State");
        }
        return this.transport.send(data);
    }
    stop() {
        return __awaiter(this, void 0, void 0, function* () {
            let previousState = this.connectionState;
            this.connectionState = 3 /* Disconnected */;
            try {
                yield this.startPromise;
            }
            catch (e) {
                // this exception is returned to the user as a rejected Promise from the start method
            }
            this.stopConnection(/*raiseClosed*/ previousState == 2 /* Connected */);
        });
    }
    stopConnection(raiseClosed, error) {
        if (this.transport) {
            this.transport.stop();
            this.transport = null;
        }
        this.connectionState = 3 /* Disconnected */;
        if (raiseClosed && this.onClosed) {
            this.onClosed(error);
        }
    }
    resolveUrl(url) {
        // startsWith is not supported in IE
        if (url.lastIndexOf("https://", 0) === 0 || url.lastIndexOf("http://", 0) === 0) {
            return url;
        }
        if (typeof window === 'undefined') {
            throw new Error(`Cannot resolve '${url}'.`);
        }
        let parser = window.document.createElement("a");
        parser.href = url;
        let baseUrl = (!parser.protocol || parser.protocol === ":")
            ? `${window.document.location.protocol}//${(parser.host || window.document.location.host)}`
            : `${parser.protocol}//${parser.host}`;
        let normalizedUrl = baseUrl + url;
        this.logger.log(ILogger_1.LogLevel.Information, `Normalizing '${url}' to '${normalizedUrl}'`);
        return normalizedUrl;
    }
}
exports.HttpConnection = HttpConnection;
