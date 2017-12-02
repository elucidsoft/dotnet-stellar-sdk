"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
Object.defineProperty(exports, "__esModule", { value: true });
class Subject {
    constructor() {
        this.observers = [];
    }
    next(item) {
        for (let observer of this.observers) {
            observer.next(item);
        }
    }
    error(err) {
        for (let observer of this.observers) {
            observer.error(err);
        }
    }
    complete() {
        for (let observer of this.observers) {
            observer.complete();
        }
    }
    subscribe(observer) {
        this.observers.push(observer);
    }
}
exports.Subject = Subject;
