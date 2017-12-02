import { DataReceived, ConnectionClosed } from "./Common";
import { IConnection } from "./IConnection";
import { IHttpConnectionOptions } from "./IHttpConnectionOptions";
export declare class HttpConnection implements IConnection {
    private connectionState;
    private url;
    private readonly httpClient;
    private readonly logger;
    private readonly options;
    private transport;
    private connectionId;
    private startPromise;
    readonly features: any;
    constructor(url: string, options?: IHttpConnectionOptions);
    start(): Promise<void>;
    private startInternal();
    private createTransport(transport, availableTransports);
    private isITransport(transport);
    private changeState(from, to);
    send(data: any): Promise<void>;
    stop(): Promise<void>;
    private stopConnection(raiseClosed, error?);
    private resolveUrl(url);
    onDataReceived: DataReceived;
    onClosed: ConnectionClosed;
}
