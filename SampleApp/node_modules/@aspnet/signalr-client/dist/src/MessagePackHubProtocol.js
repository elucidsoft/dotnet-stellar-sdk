"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
Object.defineProperty(exports, "__esModule", { value: true });
const Formatters_1 = require("./Formatters");
const msgpack5 = require("msgpack5");
class MessagePackHubProtocol {
    constructor() {
        this.name = "messagepack";
        this.type = 2 /* Binary */;
    }
    parseMessages(input) {
        return Formatters_1.BinaryMessageFormat.parse(input).map(m => this.parseMessage(m));
    }
    parseMessage(input) {
        if (input.length == 0) {
            throw new Error("Invalid payload.");
        }
        let msgpack = msgpack5();
        let properties = msgpack.decode(new Buffer(input));
        if (properties.length == 0 || !(properties instanceof Array)) {
            throw new Error("Invalid payload.");
        }
        let messageType = properties[0];
        switch (messageType) {
            case 1 /* Invocation */:
                return this.createInvocationMessage(properties);
            case 2 /* Result */:
                return this.createStreamItemMessage(properties);
            case 3 /* Completion */:
                return this.createCompletionMessage(properties);
            default:
                throw new Error("Invalid message type.");
        }
    }
    createInvocationMessage(properties) {
        if (properties.length != 5) {
            throw new Error("Invalid payload for Invocation message.");
        }
        return {
            type: 1 /* Invocation */,
            invocationId: properties[1],
            nonblocking: properties[2],
            target: properties[3],
            arguments: properties[4]
        };
    }
    createStreamItemMessage(properties) {
        if (properties.length != 3) {
            throw new Error("Invalid payload for stream Result message.");
        }
        return {
            type: 2 /* Result */,
            invocationId: properties[1],
            item: properties[2]
        };
    }
    createCompletionMessage(properties) {
        if (properties.length < 3) {
            throw new Error("Invalid payload for Completion message.");
        }
        const errorResult = 1;
        const voidResult = 2;
        const nonVoidResult = 3;
        let resultKind = properties[2];
        if ((resultKind === voidResult && properties.length != 3) ||
            (resultKind !== voidResult && properties.length != 4)) {
            throw new Error("Invalid payload for Completion message.");
        }
        let completionMessage = {
            type: 3 /* Completion */,
            invocationId: properties[1],
            error: null,
            result: null
        };
        switch (resultKind) {
            case errorResult:
                completionMessage.error = properties[3];
                break;
            case nonVoidResult:
                completionMessage.result = properties[3];
                break;
        }
        return completionMessage;
    }
    writeMessage(message) {
        switch (message.type) {
            case 1 /* Invocation */:
                return this.writeInvocation(message);
            case 2 /* Result */:
            case 3 /* Completion */:
                throw new Error(`Writing messages of type '${message.type}' is not supported.`);
            default:
                throw new Error("Invalid message type.");
        }
    }
    writeInvocation(invocationMessage) {
        let msgpack = msgpack5();
        let payload = msgpack.encode([1 /* Invocation */, invocationMessage.invocationId,
            invocationMessage.nonblocking, invocationMessage.target, invocationMessage.arguments]);
        return Formatters_1.BinaryMessageFormat.write(payload.slice());
    }
}
exports.MessagePackHubProtocol = MessagePackHubProtocol;
