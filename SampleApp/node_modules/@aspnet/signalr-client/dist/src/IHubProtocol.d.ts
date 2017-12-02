export declare const enum MessageType {
    Invocation = 1,
    Result = 2,
    Completion = 3,
}
export interface HubMessage {
    readonly type: MessageType;
    readonly invocationId: string;
}
export interface InvocationMessage extends HubMessage {
    readonly target: string;
    readonly arguments: Array<any>;
    readonly nonblocking?: boolean;
}
export interface ResultMessage extends HubMessage {
    readonly item?: any;
}
export interface CompletionMessage extends HubMessage {
    readonly error?: string;
    readonly result?: any;
}
export interface NegotiationMessage {
    readonly protocol: string;
}
export declare const enum ProtocolType {
    Text = 1,
    Binary = 2,
}
export interface IHubProtocol {
    readonly name: string;
    readonly type: ProtocolType;
    parseMessages(input: any): HubMessage[];
    writeMessage(message: HubMessage): any;
}
