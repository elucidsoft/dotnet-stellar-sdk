"use strict";
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
Object.defineProperty(exports, "__esModule", { value: true });
const HttpError_1 = require("./HttpError");
class HttpClient {
    get(url, headers) {
        return this.xhr("GET", url, headers);
    }
    options(url, headers) {
        return this.xhr("OPTIONS", url, headers);
    }
    post(url, content, headers) {
        return this.xhr("POST", url, headers, content);
    }
    xhr(method, url, headers, content) {
        return new Promise((resolve, reject) => {
            let xhr = new XMLHttpRequest();
            xhr.open(method, url, true);
            xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
            if (headers) {
                headers.forEach((value, header) => xhr.setRequestHeader(header, value));
            }
            xhr.send(content);
            xhr.onload = () => {
                if (xhr.status >= 200 && xhr.status < 300) {
                    resolve(xhr.response || xhr.responseText);
                }
                else {
                    reject(new HttpError_1.HttpError(xhr.statusText, xhr.status));
                }
            };
            xhr.onerror = () => {
                reject(new HttpError_1.HttpError(xhr.statusText, xhr.status));
            };
        });
    }
}
exports.HttpClient = HttpClient;
