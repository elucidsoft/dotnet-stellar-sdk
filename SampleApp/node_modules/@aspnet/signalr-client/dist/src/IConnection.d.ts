import { DataReceived, ConnectionClosed } from "./Common";
export interface IConnection {
    readonly features: any;
    start(): Promise<void>;
    send(data: any): Promise<void>;
    stop(): void;
    onDataReceived: DataReceived;
    onClosed: ConnectionClosed;
}
