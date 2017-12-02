import { DataReceived, TransportClosed } from "./Common";
import { IHttpClient } from "./HttpClient";
import { ILogger } from "./ILogger";
export declare enum TransportType {
    WebSockets = 0,
    ServerSentEvents = 1,
    LongPolling = 2,
}
export declare const enum TransferMode {
    Text = 1,
    Binary = 2,
}
export interface ITransport {
    connect(url: string, requestedTransferMode: TransferMode): Promise<TransferMode>;
    send(data: any): Promise<void>;
    stop(): void;
    onDataReceived: DataReceived;
    onClosed: TransportClosed;
}
export declare class WebSocketTransport implements ITransport {
    private readonly logger;
    private webSocket;
    constructor(logger: ILogger);
    connect(url: string, requestedTransferMode: TransferMode): Promise<TransferMode>;
    send(data: any): Promise<void>;
    stop(): void;
    onDataReceived: DataReceived;
    onClosed: TransportClosed;
}
export declare class ServerSentEventsTransport implements ITransport {
    private readonly httpClient;
    private readonly logger;
    private eventSource;
    private url;
    constructor(httpClient: IHttpClient, logger: ILogger);
    connect(url: string, requestedTransferMode: TransferMode): Promise<TransferMode>;
    send(data: any): Promise<void>;
    stop(): void;
    onDataReceived: DataReceived;
    onClosed: TransportClosed;
}
export declare class LongPollingTransport implements ITransport {
    private readonly httpClient;
    private readonly logger;
    private url;
    private pollXhr;
    private shouldPoll;
    constructor(httpClient: IHttpClient, logger: ILogger);
    connect(url: string, requestedTransferMode: TransferMode): Promise<TransferMode>;
    private poll(url, transferMode);
    send(data: any): Promise<void>;
    stop(): void;
    onDataReceived: DataReceived;
    onClosed: TransportClosed;
}
